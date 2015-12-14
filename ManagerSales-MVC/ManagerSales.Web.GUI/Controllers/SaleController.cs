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
    public class SaleController : Controller
    {
        private readonly IModelHandler<Customer> _customerHandler;
        private readonly IModelHandler<Manager> _managerHandler;
        private readonly IModelHandler<Product> _productHandler;
        private readonly IModelHandler<Sale> _saleHandler;

        public SaleController()
        {
            _saleHandler = new SaleModelHandler();
            _customerHandler = new CustomerModelHandler();
            _managerHandler = new ManagerModelHandler();
            _productHandler = new ProductModelHandler();

            Mapper.CreateMap<Customer, Models.ManagerSalesModels.Customer>();
            Mapper.CreateMap<Product, Models.ManagerSalesModels.Product>();
            Mapper.CreateMap<Manager, Models.ManagerSalesModels.Manager>();
            Mapper.CreateMap<Sale, Models.ManagerSalesModels.Sale>();

            Mapper.CreateMap<Models.ManagerSalesModels.Customer, Customer>();
            Mapper.CreateMap<Models.ManagerSalesModels.Product, Product>();
            Mapper.CreateMap<Models.ManagerSalesModels.Manager, Manager>();
            Mapper.CreateMap<Models.ManagerSalesModels.Sale, Sale>();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Sales",
                _saleHandler.GetAll().Select(Mapper.Map<Sale, Models.ManagerSalesModels.Sale>).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SaleGrid()
        {
            return View("SaleGrid", _saleHandler.GetAll().Select(Mapper.Map<Sale, Models.ManagerSalesModels.Sale>).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSale()
        {
            ViewBag.Products = Mapper.Map<IEnumerable<Product>, IEnumerable<Models.ManagerSalesModels.Product>>(
                _productHandler.GetAll()).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
            ViewBag.Customers = Mapper.Map<IEnumerable<Customer>, IEnumerable<Models.ManagerSalesModels.Customer>>(
                _customerHandler.GetAll()).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
            ViewBag.Managers = Mapper.Map<IEnumerable<Manager>, IEnumerable<Models.ManagerSalesModels.Manager>>(
                _managerHandler.GetAll()).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.LastName
                });
            return View("AddSale", new Models.ManagerSalesModels.Sale());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSale(Models.ManagerSalesModels.Sale sale)
        {
            try
            {
                _saleHandler.Add(Mapper.Map<Models.ManagerSalesModels.Sale, Sale>(sale));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("AddSale", new Models.ManagerSalesModels.Sale());
            }

            return RedirectToAction("Index", "Sale");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult EditSale(string id)
        {
            int value;
            int.TryParse(id, out value);
            var products =
                Mapper.Map<IEnumerable<Product>, IEnumerable<Models.ManagerSalesModels.Product>>(
                    _productHandler.GetAll()).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
            var customers =
                Mapper.Map<IEnumerable<Customer>, IEnumerable<Models.ManagerSalesModels.Customer>>(
                    _customerHandler.GetAll()).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
            var managers =
                Mapper.Map<IEnumerable<Manager>, IEnumerable<Models.ManagerSalesModels.Manager>>(
                    _managerHandler.GetAll()).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.LastName
                    });

            ViewBag.Products = products;
            ViewBag.Customers = customers;
            ViewBag.Managers = managers;

            return View("EditSale", Mapper.Map<Sale, Models.ManagerSalesModels.Sale>(_saleHandler.GetItemById(value)));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult EditSale(Models.ManagerSalesModels.Sale sale)
        {
            try
            {
                _saleHandler.Update(Mapper.Map<Models.ManagerSalesModels.Sale, Sale>(sale));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("EditSale", sale);
            }

            return RedirectToAction("Index", "Sale");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveSale(string id)
        {
            int value;
            int.TryParse(id, out value);

            _saleHandler.Remove(_saleHandler.GetItemById(value));

            return RedirectToAction("Index", "Sale");
        }
    }
}