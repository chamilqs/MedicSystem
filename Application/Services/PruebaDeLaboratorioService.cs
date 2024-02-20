using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.PruebasDeLaboratorio;
using MedicSystem.Core.Domain.Entities;

namespace MedicSystem.Core.Application.Services
{
    public class PruebaDeLaboratorioService : IPruebaDeLaboratorioService
    {
        private readonly IPruebaDeLaboratorioRepository _pruebaRepository;

        public PruebaDeLaboratorioService(IPruebaDeLaboratorioRepository pruebaRepository)
        {
            _pruebaRepository = pruebaRepository;
        }

        public async Task<SavePruebaDeLaboratorioViewModel> Add(SavePruebaDeLaboratorioViewModel vm)
        {        
            PruebaDeLaboratorio prueba = new();

            prueba.Id = vm.Id;
            prueba.Nombre = vm.Nombre;

            prueba = await _pruebaRepository.AddAsync(prueba);

            if (prueba == null)
            {
                return null;
            }
            else
            {
                SavePruebaDeLaboratorioViewModel vmUsuario = new();

                vmUsuario.Id = prueba.Id;
                vmUsuario.Nombre = prueba.Nombre;

                return vmUsuario;
            }
            
        }
        
        public async Task Update(SavePruebaDeLaboratorioViewModel vm)
        {
            PruebaDeLaboratorio prueba = await _pruebaRepository.GetByIdAsync(vm.Id);
            prueba.Nombre = vm.Nombre;

            await _pruebaRepository.UpdateAsync(prueba);

        }

        public async Task Delete(int id)
        {
            var product = await _pruebaRepository.GetByIdAsync(id);
            await _pruebaRepository.DeleteAsync(product);
        }

        public async Task<List<PruebaDeLaboratorioViewModel>> GetAllViewModel()
        {

            var pruebaList = await _pruebaRepository.GetAllAsync();

            return pruebaList.Select(user => new PruebaDeLaboratorioViewModel
            {
                Id = user.Id,
                Nombre = user.Nombre,

            }).ToList();
        }

        public async Task<SavePruebaDeLaboratorioViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _pruebaRepository.GetByIdAsync(id);

            SavePruebaDeLaboratorioViewModel vm = new();

            vm.Id = user.Id;
            vm.Nombre = user.Nombre;

            return vm;
        }

    }
}
