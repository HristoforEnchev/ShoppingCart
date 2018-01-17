namespace ShoppingCartDemo.Services
{
    using ShoppingCartDemo.Services.Models;
    using System.Collections.Generic;

    public  interface IShoppingCartManager
    {
        void AddToCart(string id, int productId);

        void RemoveFromCart(string id, int productId);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
