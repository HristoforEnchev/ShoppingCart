using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartDemo.Services.Models
{
    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);


        public void AddToCart(int productId)    //CartItem item
        {
            var cartItem = this.items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
            {
                this.items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity ++;
            }
        }

        public void RemoveFromCart(int productId)
        {
            if (this.items.Any(i => i.ProductId == productId))
            {
                var item = this.items.First(i => i.ProductId == productId);

                this.items.Remove(item);
            }
        }

    }
}
