using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWebApp.Entities;
using ShoppingWebApp.Repositories.Interfaces;

namespace ShoppingWebApp.Pages;

public class ProductModel : PageModel
{
    private readonly IProductRepository _productRepository;
    private readonly ICartRepository _cartRepository;

    public ProductModel(IProductRepository productRepository, ICartRepository cartRepository)
    {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
    }

    public IEnumerable<Category> CategoryList { get; set; } = new List<Category>();
    public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; }

    public async Task<IActionResult> OnGetAsync(int? categoryId)
    {
        CategoryList = await _productRepository.GetCategories();

        if (categoryId.HasValue)
        {
            ProductList = await _productRepository.GetProductByCategory(categoryId.Value);
            SelectedCategory = CategoryList.FirstOrDefault(c => c.Id == categoryId.Value)?.Name;
        }
        else
        {
            ProductList = await _productRepository.GetProducts();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        //if (!User.Identity.IsAuthenticated)
        //    return RedirectToPage("./Account/Login", new { area = "Identity" });

        await _cartRepository.AddItem("test", productId);
        return RedirectToPage("Cart");
    }
}
