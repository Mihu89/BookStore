using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetail shippingDetail);
    }
}
