using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecification<TEntity , Tkey> where  TEntity : BaseEntity<Tkey>
    {
        // why  not make set becuase i want control  about who  can adding  by  only  calling 
        //  i  mean  not allow any  componets  not calling this can  see  this prop .  
        public ICollection<Expression<Func<TEntity ,  object>>> IncludeExpressions { get; }
        public Expression<Func<TEntity  ,  bool>> Condition { get;  }
        
        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }
        
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; }
    }
}
