using Microsoft.EntityFrameworkCore;
using ShoppingWebApp.Data;
using ShoppingWebApp.Entities;
using ShoppingWebApp.Repositories.Interfaces;

namespace ShoppingWebApp.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShoppingContext _dbContext;

    public OrderRepository(ShoppingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> CheckOut(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await _dbContext.Orders
                        .Where(o => o.UserName == userName)
                        .ToListAsync();

        return orderList;
    }
}