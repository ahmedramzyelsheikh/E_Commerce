using E_Commerce.Application.Common;
using System.Diagnostics.Contracts;

namespace E_Commerce.API.Common
{
    public class Results
    {
        public bool  IsSuccessed { get;  }
        
        public IReadOnlyList<Errors> Error { get;  }

        public Results(bool IsSuccessed , IReadOnlyList<Errors> Error)
        {
            this.IsSuccessed = IsSuccessed;
            this.Error = Error; 
        }
        public static Results OK() => new Results(true, Array.Empty<Errors>());
        public static Results Fail(Errors error) => new Results(false, new Errors[] { error });

        public static Results Fail(IReadOnlyList<Errors> error) => new Results(false, error);

    }
    public class Results<TVaule> : Results
    {

        public TVaule Data => IsSuccessed ? _value : throw new InvalidOperationException("Cannot  assign data  because this  operations is failure");
        private TVaule _value; 

        public Results(TVaule  value ) :  base(true  , Array.Empty<Errors>())
        {
            _value = value;
        }
        public Results(Errors error) : base(false, new Errors[] { error })
        {
            _value = default;
        }
        public Results(IReadOnlyList<Errors> error ) : base(false, error)
        {
            _value = default;
        }

        public static   Results<TVaule> OK(TVaule DaTa ) => new Results<TVaule>(DaTa);
        public static  Results<TVaule> Fail(Errors error) => new Results<TVaule>( new Errors[] { error });

        public static  Results<TVaule> Fail(IReadOnlyList<Errors> error) => new Results<TVaule>( error);

    }

}
