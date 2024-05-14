using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Infrastructure.Repositories;

namespace TESAmerica.Infrastructure.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TESAmericaContext _context;
        public IPedidoRepository PedidoRepository => new PedidoRepository(_context);
        public IComisionesRepository ComisionesRepository => new ComisionesRepository(_context);
        
        public UnitOfWork(TESAmericaContext context)
        {
            _context = context;
        }
        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
