using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> productRepository;
        IRepository<ProductCategory> productCategoryRepository;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            productRepository = productContext;
            productCategoryRepository = productCategoryContext;
        }

        public ActionResult Index(string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategoryRepository.Collection().ToList();
            if (Category == null)
            {
                products = productRepository.Collection().ToList();
            }
            else
            {
                products = productRepository.Collection().Where(p => p.Category == Category).ToList();
            }
            ProductListViewModel viewModel = new ProductListViewModel();
            viewModel.Products = products;
            viewModel.ProductCategories = categories;

            return View(viewModel);
        }

        public ActionResult Details(string id)
        {
            Product product = productRepository.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

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