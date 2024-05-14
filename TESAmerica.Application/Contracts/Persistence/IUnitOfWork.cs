using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESAmerica.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IPedidoRepository PedidoRepository { get; }
        IComisionesRepository ComisionesRepository { get; }
        IGenericRepository<T> GenericRepository<T>() where T : class;
        Task BeginTransaction();

        Task Save();
        Task Commit();
        Task Rollback();
    }
}
