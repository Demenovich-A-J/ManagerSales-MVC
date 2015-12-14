using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ManagerSales.BL.Models;
using ManagerSales.BL.ModelsHandlers;
using ManagerSales.BL.ModelsHandlers.Intarfaces;

namespace ManagerSales.Web.GUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IModelHandler<Product> _productHandler;

        public ProductController()
        {
            _productHandler = new ProductModelHandler();
            Mapper.CreateMap<Product, Models.ManagerSalesModels.Product>();
            Mapper.CreateMap<Models.ManagerSalesModels.Product, Product>();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Products",
                _productHandler.GetList(x => true).Select(Mapper.Map<Product, Models.ManagerSalesModels.Product>).ToList());
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ProductGrid()
        {
            return View("ProductGrid", _productHandler.GetList(x => true).Select(Mapper.Map<Product, Models.ManagerSalesModels.Product>).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct()
        {
            return View("AddProduct", new Models.ManagerSalesModels.Product());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(Models.ManagerSalesModels.Product product)
        {
            try
            {
                _productHandler.Add(Mapper.Map<Models.ManagerSalesModels.Product, Product>(product));
            }
            catch (ArgumentNullException ex)
            {
                return View("AddProduct", new Models.ManagerSalesModels.Product());
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(string id)
        {
            int value;
            int.TryParse(id, out value);
            var products =
                Mapper.Map<IEnumerable<Product>, IEnumerable<Models.ManagerSalesModels.Product>>(
                    _productHandler.GetList(x => true)).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
            ViewBag.Products = products;

            return View("EditProduct",
                Mapper.Map<Product, Models.ManagerSalesModels.Product>(_productHandler.GetItemById(value)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(Models.ManagerSalesModels.Product product)
        {
            try
            {
                _productHandler.Update(Mapper.Map<Models.ManagerSalesModels.Product, Product>(product));
            }
            catch (ArgumentNullException ex)
            {
                return View("EditProduct", new Models.ManagerSalesModels.Product());
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveProduct(string id)
        {
            int value;
            int.TryParse(id, out value);

            _productHandler.Remove(_productHandler.GetItemById(value));

            return RedirectToAction("Index", "Product");
        }
    }
}