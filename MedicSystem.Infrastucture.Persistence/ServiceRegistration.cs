using MedicSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Infrastructure.Persistence.Contexts;
using MedicSystem.Infrastucture.Persistence.Repositories;

namespace MedicSystem.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m=> m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositorios
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPruebaDeLaboratorioRepository, PruebaDeLaboratorioRepository>(); 
            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IPacienteRepository, PacienteRepository>();
            services.AddTransient<ICitaRepository, CitaRepository>();
            #endregion
        }
    }
}
