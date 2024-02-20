using Microsoft.Extensions.DependencyInjection;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.Services;
using Microsoft.Extensions.Configuration;

namespace MedicSystem.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {   
            #region Servicios
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IPruebaDeLaboratorioService, PruebaDeLaboratorioService>();
            services.AddTransient<IMedicoService, MedicoService>();
            services.AddTransient<IPacienteService, PacienteService>();
            services.AddTransient<ICitaService, CitaService>();
            #endregion
        }
    }
}
