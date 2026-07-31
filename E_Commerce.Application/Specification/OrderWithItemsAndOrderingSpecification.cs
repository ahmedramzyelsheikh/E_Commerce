using E_Commerce.Domain.Models.OrderModule;
using System.Linq.Expressions;

namespace E_Commerce.Application.Specification
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order, int>
    {
        public OrderWithItemsAndOrderingSpecification(string email)
            : base(o => o.BuyerEmail == email)
        {
            Adding(o => o.DeliveryMethod!);
            Adding(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemsAndOrderingSpecification(int id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            Adding(o => o.DeliveryMethod!);
            Adding(o => o.Items);
        }
    }
}
