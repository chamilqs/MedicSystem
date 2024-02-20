using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.Interfaces.Services;
using MedicSystem.Core.Application.ViewModels.ResultadosDeLaboratorio;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicSystem.Core.Application.Services
{
    public class ResultadoDeLaboratorioService : IResultadoDeLaboratorioService
    {
        private readonly IResultadoDeLaboratorioRepository _resultadoDeLaboratorioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioViewModel userViewModel;

        public ResultadoDeLaboratorioService(IResultadoDeLaboratorioRepository resultadoDeLaboratorioRepository, IHttpContextAccessor httpContextAccessor)
        {
            _resultadoDeLaboratorioRepository = resultadoDeLaboratorioRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("CurrentUser");
        }

        public Task<SaveResultadoDeLaboratorioViewModel> Add(SaveResultadoDeLaboratorioViewModel vm)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultadoDeLaboratorioViewModel>> GetAllViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<SaveResultadoDeLaboratorioViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(SaveResultadoDeLaboratorioViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
