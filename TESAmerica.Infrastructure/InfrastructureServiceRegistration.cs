using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Infrastructure.Presistence;
using TESAmerica.Infrastructure.Repositories;

namespace TESAmerica.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TESAmericaContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionDB")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IPedidoRepository), typeof(PedidoRepository));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            return services;
        }
    }
}
