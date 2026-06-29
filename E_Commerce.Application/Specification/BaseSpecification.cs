using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specification
{
    public abstract class BaseSpecification<TEntity, Tkey> :
        ISpecification<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get;  } = [];

        public Expression<Func<TEntity, bool>> Condition { get; private set;  }

        protected BaseSpecification(Expression<Func<TEntity, bool>> _Condition)
        {
            this.Condition = _Condition;
        }

        public  void  Adding(Expression<Func<TEntity ,  object>> expression)
        {
            IncludeExpressions.Add(expression);  
        }
    }
}
