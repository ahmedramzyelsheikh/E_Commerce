using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEnity , Tkey> where  TEnity:  BaseEntity<Tkey>
    {
        void Add(TEnity Entity);
        void Delete(TEnity Entity);
        void Update(TEnity Entity);

        Task<TEnity> GetById(int id, CancellationToken ct);

        Task<IEnumerable<TEnity>> GetAll(CancellationToken ct);
        Task<IEnumerable<TEnity>> GetAll( ISpecification<TEnity  ,  Tkey> spec , CancellationToken ct);
        Task<TEnity> GetById(ISpecification<TEnity  , Tkey> spec,  CancellationToken ct);

    }
}
