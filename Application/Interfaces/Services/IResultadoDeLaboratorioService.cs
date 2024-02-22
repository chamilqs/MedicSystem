using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Application.Interfaces.Services
{
    public interface IResultadoDeLaboratorioService : IGenericService<SaveResultadoDeLaboratorioViewModel, ResultadoDeLaboratorioViewModel>
    {
        Task UpdateState(int id, EstadoResultado estadoResultado);
        Task<List<ResultadoDeLaboratorioViewModel>> GetByCedulaAsync(string cedula);
        Task UpdateResultado(int id, string resultado);
        Task<List<ResultadoDeLaboratorioViewModel>> GetByCitaIdAsync(int citaId);
    }
}
