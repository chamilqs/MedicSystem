using MedicSystem.Core.Application.Helpers;
using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Application.ViewModels.Usuarios;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Infrastructure.Persistence.Contexts;
using MedicSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicSystem.Infrastucture.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationContext _dbContext;

        public UsuarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypted = PasswordEncryption.Encrypt256Hash(loginVm.Password);
            Usuario user = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == loginVm.NombreUsuario && x.Password == passwordEncrypted);

            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public override async Task<Usuario> AddAsync(Usuario entity)
        {
            entity.Password = PasswordEncryption.Encrypt256Hash(entity.Password);
            var user = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == entity.NombreUsuario);

            if (user == null)
            {
                return await base.AddAsync(entity);
            }
            else
            {
                return null;

            }

        }

        public override async Task UpdateAsync(Usuario entity)
        {
            entity.Password = PasswordEncryption.Encrypt256Hash(entity.Password);
            await base.UpdateAsync(entity);
        }

    }
}
