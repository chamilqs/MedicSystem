using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Infrastructure.Persistence.Contexts;
using MedicSystem.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicSystem.Infrastucture.Persistence.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
    {
        private readonly ApplicationContext _dbContext;

        public MedicoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
