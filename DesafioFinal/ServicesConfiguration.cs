using DesafioFinal.Data;
using DesafioFinal.Repositorio.MessageRepo;
using DesafioFinal.Repositorio.SubscriptionRepo;
using DesafioFinal.Repositorio.UserRepo;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioFinal
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddScoped<ISubscriptionRepo, SubscriptionRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IMessageConfiguration, MessageConfiguration>();
            return services;
        }
    }
}
