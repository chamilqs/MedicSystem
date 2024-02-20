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
    public class PruebaDeLaboratorioRepository : GenericRepository<PruebaDeLaboratorio>, IPruebaDeLaboratorioRepository
    {
        private readonly ApplicationContext _dbContext;

        public PruebaDeLaboratorioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<PruebaDeLaboratorio> AddAsync(PruebaDeLaboratorio entity)
        {
            var prueba = await _dbContext.PruebasDeLaboratorio.FirstOrDefaultAsync(x => x.Nombre == entity.Nombre);

            if (prueba == null)
            {
                return await base.AddAsync(entity);
            }
            else
            {
                return null;

            }

        }

        public override async Task UpdateAsync(PruebaDeLaboratorio entity)
        {
            // var prueba = await _dbContext.PruebasDeLaboratorio.FirstOrDefaultAsync(x => x.Nombre == entity.Nombre);
            await base.UpdateAsync(entity);
            
        }

    }
}
