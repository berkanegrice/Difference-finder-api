using System.Reflection;
using Diff.Application.Mapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Diff.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}