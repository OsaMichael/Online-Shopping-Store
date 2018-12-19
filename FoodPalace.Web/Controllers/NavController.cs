using FoodPalace.Infrastructure.Intaface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodPalace.Web.Controllers
{

    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Nav

        public PartialViewResult Menu(string category = null)
        {
           //var query = from c in repository.Products select c;
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);



        }
        // NOTE:
        // The SELECT DISTINCT statement is used to return only distinct(different) values.
        //Inside a table, a column often contains many duplicate values;
        //and sometimes you only want to list the different(distinct) values.

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}