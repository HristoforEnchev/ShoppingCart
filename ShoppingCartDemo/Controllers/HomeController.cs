using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartDemo.Data;
using ShoppingCartDemo.Data.Models;
using ShoppingCartDemo.Models;
using System.Diagnostics;
using System.Linq;

namespace ShoppingCartDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            //var db = (ApplicationDbContext)this.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext));

            //for (int i = 1; i < 21; i++)
            //{
            //    this.db.Products.Add(new Product
            //    {
            //        Title = $"Product {i}",
            //        Price = i * 20
            //    });
            //}

            //this.db.SaveChanges();

            var model = this.db.Products.OrderBy(p => p.Price).ToList();

            
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
