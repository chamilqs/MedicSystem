using MedicSystem.Core.Application.ViewModels.Citas;
using MedicSystem.Core.Domain.Enum;

namespace MedicSystem.Core.Application.Interfaces.Services
{
    public interface ICitaService : IGenericService<SaveCitasViewModel, CitaViewModel>
    {
        Task UpdateState(int id, EstadoCita estadoCita);
    }
}
