namespace ShoppingCartDemo.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShoppingCartDemo.Data;
    using ShoppingCartDemo.Data.Models;
    using ShoppingCartDemo.Models;
    using ShoppingCartDemo.Models.ShoppingCart;
    using ShoppingCartDemo.Services;
    using System.Linq;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager, 
                                      ApplicationDbContext db, 
                                      UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.db = db;
            this.userManager = userManager;
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

        [Authorize]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartManager.GetItems(shoppingCartId);

            var userId = this.userManager.GetUserId(User);

            var order = new Order()
            {
                ApplicationUserId = userId
            };

            foreach (var item in items)
            {
                var productId = item.ProductId;
                var quantity = item.Quantity;

                var orderProduct = new OrderProduct()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ProductPrice = this.db.Products.Where(p => p.Id == productId).Select(p => p.Price).FirstOrDefault()
                };

                order.OrderProducts.Add(orderProduct);
                order.TotalPrice += (this.db.Products.FirstOrDefault(p => p.Id == productId).Price) * quantity;
            }

            this.db.Orders.Add(order);
            this.db.SaveChanges();

            this.shoppingCartManager.Clear(shoppingCartId);

            return RedirectToAction(nameof(HomeController.Index), "HomeController");
        }
    }
}
