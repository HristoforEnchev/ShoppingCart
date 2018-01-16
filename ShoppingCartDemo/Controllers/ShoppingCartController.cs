namespace ShoppingCartDemo.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using ShoppingCartDemo.Data;
    using ShoppingCartDemo.Models.ShoppingCart;
    using ShoppingCartDemo.Services;
    using System.Linq;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly ApplicationDbContext db;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager, ApplicationDbContext db)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.db = db;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = shoppingCartManager.GetItems(shoppingCartId);

            var itemsIds = items.Select(i => i.ProductId);

            var cartItems = this.db
                .Products
                .Where(p => itemsIds.Contains(p.Id))
                .Select(p => new ShoppingCartItemsViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Quantity = items.Where(i => i.ProductId == p.Id).Select(i => i.Quantity).FirstOrDefault()
                })
                .ToList();

            return View(cartItems);
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();


            this.shoppingCartManager.AddToCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }
    }
}
