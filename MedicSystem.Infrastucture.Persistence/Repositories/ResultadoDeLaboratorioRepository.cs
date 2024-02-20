using MedicSystem.Core.Application.Interfaces.Repositories;
using MedicSystem.Core.Domain.Entities;
using MedicSystem.Infrastructure.Persistence.Contexts;
using MedicSystem.Infrastructure.Persistence.Repositories;

namespace MedicSystem.Infrastucture.Persistence.Repositories
{
    public class ResultadoDeLaboratorioRepository : GenericRepository<ResultadoDeLaboratorio>, IResultadoDeLaboratorioRepository
    {
        private readonly ApplicationContext _dbContext;

        public ResultadoDeLaboratorioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
