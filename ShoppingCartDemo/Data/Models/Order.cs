using ShoppingCartDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        //Address

        public decimal TotalPrice { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();


    }
}
