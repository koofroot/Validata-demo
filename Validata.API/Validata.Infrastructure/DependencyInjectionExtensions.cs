using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Validata.Infrastructure
{
    public static partial class DependencyInjectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
