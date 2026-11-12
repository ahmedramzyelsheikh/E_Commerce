using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsynce(CancellationToken ct);

        IGenericRepository<TEnitiy, Tkey> GetRepository<TEnitiy, Tkey>() where TEnitiy : BaseEntity<Tkey>;
    }
}
