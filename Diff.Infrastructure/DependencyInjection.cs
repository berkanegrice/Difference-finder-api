using Diff.Application.Interfaces;
using Diff.Application.Interfaces.Repositories;
using Diff.Application.Interfaces.Repositories.Base;
using Diff.Domain.Entities;
using Diff.Infrastructure.Data;
using Diff.Infrastructure.Files;
using Diff.Infrastructure.Repositories;
using Diff.Infrastructure.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Diff.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IInputRepository, InputRepository>();
            services.AddTransient<IDifferencesFinder, DifferencesFinder>();
            services.AddSingleton<InputContext<InputModel>>();
            
            return services;
        }
    }
}