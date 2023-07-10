using ShoppingWebApp.Entities;

namespace ShoppingWebApp.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order> CheckOut(Order order);
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}
