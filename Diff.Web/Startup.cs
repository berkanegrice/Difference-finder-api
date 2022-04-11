using Diff.Application;
using Diff.Application.Interfaces;
using Diff.Domain.Entities;
using Diff.Domain.Repositories;
using Diff.Domain.Repositories.Base;
using Diff.Infrastructure.Data;
using Diff.Infrastructure.Files;
using Diff.Infrastructure.Repositories;
using Diff.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Diff.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddControllers();
            services.AddSingleton<InputContext<InputModel>>(); // move to another project.

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diff.Web", Version = "v1" });
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // move to another project.
            services.AddTransient<IInputRepository, InputRepository>(); // move to another project.
            services.AddTransient<IDifferencesFinder, DifferencesFinder>(); // move to another project.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diff.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}