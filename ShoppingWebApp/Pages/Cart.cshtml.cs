using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebApp.Entities;
using ShoppingWebApp.Repositories.Interfaces;

namespace ShoppingWebApp.Pages;

public class CartModel : PageModel
{
    private readonly ICartRepository _cartRepository;

    public CartModel(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public Cart Cart { get; set; } = new Cart();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await _cartRepository.GetCartByUserName("test");

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(int cartId, int cartItemId)
    {
        await _cartRepository.RemoveItem(cartId, cartItemId);
        return RedirectToPage();
    }
}
