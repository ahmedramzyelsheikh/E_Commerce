using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specification
{
    public static class SpecificationEvallutor
    {
        public static IQueryable<TEntity> CreateQuery<TEntity , Tkey>(IQueryable<TEntity> EntryPoint  ,  ISpecification<TEntity , Tkey> spec)
        where  TEntity :  BaseEntity<Tkey>
        {
            var query = EntryPoint;

            if(spec.Condition != null)
                  query  = query.Where(spec.Condition);


            if (spec.IncludeExpressions.Any()) query = spec.IncludeExpressions.Aggregate(query, (cur, next) => cur.Include(next));

            return query; 

        }
    }
}
