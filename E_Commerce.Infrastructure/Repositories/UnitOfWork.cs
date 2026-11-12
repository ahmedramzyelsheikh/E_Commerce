using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {

        //  create Dictionary  to  make cashing  
        private readonly Dictionary<String, object> repos = []; 
        public IGenericRepository<TEnitiy, Tkey> GetRepository<TEnitiy, Tkey>() where TEnitiy : BaseEntity<Tkey>
        {
            var type = typeof(TEnitiy).Name; 
            if(repos.TryGetValue(type ,  out object? value))
            {
                return (IGenericRepository<TEnitiy, Tkey>)value; 

            }
            var obj = new GenericRepository<TEnitiy,Tkey>(dbContext);
            repos[type] = obj;
            return obj;

        }

        public async Task<int> SaveChangeAsynce(CancellationToken ct)
                    => await dbContext.SaveChangesAsync(ct);
    }
}
