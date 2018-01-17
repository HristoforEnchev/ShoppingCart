using System.Collections.Concurrent;
using System.Collections.Generic;
using ShoppingCartDemo.Services.Models;
using System.Linq;

namespace ShoppingCartDemo.Services.Implementaion
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        public readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartManager()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }


        public void AddToCart(string id, int productId)   //CartItem cartItem
        {
            var shopingCart = GetShoppingCart(id);

            shopingCart.AddToCart(productId);
        }

        public void RemoveFromCart(string id, int productId)
        {
            var shopingCart = GetShoppingCart(id);

            shopingCart.RemoveFromCart(productId);
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shopingCart = GetShoppingCart(id);

            return shopingCart.Items.ToList();
        }

        public void Clear(string id)
        {
            this.GetShoppingCart(id).Clear();
        }


        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }

        
    }
}
