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

            if (spec.Condition != null)
                  query  = query.Where(spec.Condition);

            if (spec.OrderBy != null)
                  query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null)
                  query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagingEnabled)
                  query = query.Skip(spec.Skip).Take(spec.Take);

            if (spec.IncludeExpressions.Any()) query = spec.IncludeExpressions.Aggregate(query, (cur, next) => cur.Include(next));

            return query; 

        }
    }
}
