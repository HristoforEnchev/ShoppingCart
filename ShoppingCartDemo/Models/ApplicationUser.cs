namespace ShoppingCartDemo.Models
{
    using Microsoft.AspNetCore.Identity;
    using ShoppingCartDemo.Data.Models;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
