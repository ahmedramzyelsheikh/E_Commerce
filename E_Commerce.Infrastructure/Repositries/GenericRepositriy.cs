using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositries
{
    internal class GenericRepositriy<TEntity, Tkey>(StoreDbContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity Entity)
             => dbContext.Set<TEntity>().Add(Entity); 
       

        public void Delete(TEntity Entity)
       => dbContext.Set<TEntity>().Remove(Entity);

        public async Task<IEnumerable<TEntity?>> GetAll(CancellationToken ct)
             => await dbContext.Set<TEntity>().ToListAsync(ct); 
       

        public async Task<TEntity?> GetById(int id, CancellationToken ct)=>
            await dbContext.Set<TEntity>().FindAsync(id, ct); 
        

        public void Update(TEntity Entity)
         => dbContext.Set<TEntity>().Update(Entity);
    }
}
