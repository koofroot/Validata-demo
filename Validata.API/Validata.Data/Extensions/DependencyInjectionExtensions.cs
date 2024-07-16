using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Validata.Data.Interfaces;
using Validata.Data.Repositories;

namespace Validata.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<ValidataContext>(options =>
                options.UseSqlServer(connectionString));
        }
        public static void AddData(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IReadonlyRepository<>), typeof(ReadonlyRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
