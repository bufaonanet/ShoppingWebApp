using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebApp.Entities;
using ShoppingWebApp.Repositories.Interfaces;

namespace ShoppingWebApp.Pages;

public class OrderModel : PageModel
{
    private readonly IOrderRepository _orderRepository;

    public OrderModel(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IEnumerable<Order> Orders { get; set; } = new List<Order>();

    public async Task<IActionResult> OnGetAsync()
    {
        Orders = await _orderRepository.GetOrdersByUserName("test");

        return Page();
    }
}