using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> productCategoryRepository;

        public ProductCategoryManagerController()
        {
            productCategoryRepository = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productsCategories = productCategoryRepository.Collection().ToList();
            return View(productsCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                productCategoryRepository.Insert(productCategory);
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategoryToEdit = productCategoryRepository.Find(id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToEdit);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToEdit = productCategoryRepository.Find(id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    productCategoryToEdit.Category = productCategory.Category;
                    productCategoryRepository.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategoryRepository.Find(id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategoryToDelete = productCategoryRepository.Find(id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                productCategoryRepository.Delete(id);
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}