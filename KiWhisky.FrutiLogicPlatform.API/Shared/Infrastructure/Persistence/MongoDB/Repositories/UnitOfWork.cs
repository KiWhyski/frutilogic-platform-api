using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Repositories;
using KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace KiWhisky.FrutiLogicPlatform.API.Shared.Infrastructure.Persistence.MongoDB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
        }
    }
}

