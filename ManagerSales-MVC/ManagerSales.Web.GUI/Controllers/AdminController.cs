using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ManagerSales.BL.ModelsHandlers;
using ManagerSales.BL.ModelsHandlers.Intarfaces;
using ManagerSales.Web.GUI.Models.ManagerSalesModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Customer = ManagerSales.BL.Models.Customer;
using Manager = ManagerSales.BL.Models.Manager;
using Product = ManagerSales.BL.Models.Product;
using Sale = ManagerSales.BL.Models.Sale;

namespace ManagerSales.Web.GUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IModelHandler<Customer> _customerHandler;
        private readonly IModelHandler<Manager> _managerHandler;
        private readonly IModelHandler<Product> _productHandler;
        private readonly IModelHandler<Sale> _saleHandler;

        public AdminController()
        {
            _saleHandler = new SaleModelHandler();
            _managerHandler = new ManagerModelHandler();
            _productHandler = new ProductModelHandler();
            _customerHandler = new CustomerModelHandler();

            Mapper.CreateMap<Customer, Models.ManagerSalesModels.Customer>();
            Mapper.CreateMap<Product, Models.ManagerSalesModels.Product>();
            Mapper.CreateMap<Manager, Models.ManagerSalesModels.Manager>();
            Mapper.CreateMap<Sale, ExSale>();

            Mapper.CreateMap<Models.ManagerSalesModels.Customer, Customer>();
            Mapper.CreateMap<Models.ManagerSalesModels.Product, Product>();
            Mapper.CreateMap<Models.ManagerSalesModels.Manager, Manager>();
            Mapper.CreateMap<ExSale, Sale>();


            Mapper.CreateMap<Sale, Models.ManagerSalesModels.Sale>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.Name)
                )
                .ForMember(
                    dest => dest.ManagerName,
                    opt => opt.MapFrom(src => src.Manager.LastName)
                )
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name)
                );
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UsersGrid()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ViewBag.Manager = userManager;
            return PartialView("PartialGrid/UserGrid", userManager.Users.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct()
        {
            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
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
                return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
            }

            return RedirectToAction("Products", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddManager()
        {
            return View("Add/AddManager", new Models.ManagerSalesModels.Manager());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddManager(Models.ManagerSalesModels.Manager manager)
        {
            try
            {
                _managerHandler.Add(Mapper.Map<Models.ManagerSalesModels.Manager, Manager>(manager));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddManager", new Models.ManagerSalesModels.Manager());
            }

            return RedirectToAction("Managers", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCustomer()
        {
            return View("Add/AddCustomer", new Models.ManagerSalesModels.Customer());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCustomer(Models.ManagerSalesModels.Customer customer)
        {
            try
            {
                _customerHandler.Add(Mapper.Map<Models.ManagerSalesModels.Customer, Customer>(customer));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddCustomer", new Models.ManagerSalesModels.Customer());
            }

            return RedirectToAction("Customers", "Home");
        }

        /*****************************************************************/
        //work on add sale
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSale()
        {
            return View("Add/AddSale", new ExSale());
        }

        /*****************************************************************/
        //work on add sale
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSale(ExSale exSale)
        {
            try
            {
                _saleHandler.Add(Mapper.Map<ExSale, Sale>(exSale));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddSale", new ExSale());
            }

            return RedirectToAction("Index", "Home");
        }

        /**********************************************************************/

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            return View("Add/AddUser");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser(ApplicationUserManager u)
        {
            return View("Edit/EditUser");
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

            return View("Edit/EditProduct",
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
                return View("Edit/EditProduct", new Models.ManagerSalesModels.Product());
            }

            return RedirectToAction("Products", "Home");
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult EditSale(string id)
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
            var customers =
                Mapper.Map<IEnumerable<Customer>, IEnumerable<Models.ManagerSalesModels.Customer>>(
                    _customerHandler.GetList(x => true)).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
            var managers =
                Mapper.Map<IEnumerable<Manager>, IEnumerable<Models.ManagerSalesModels.Manager>>(
                    _managerHandler.GetList(x => true)).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.LastName
                    });

            ViewBag.Products = products;
            ViewBag.Customers = customers;
            ViewBag.Managers = managers;

            return View("Edit/EditSale", Mapper.Map<Sale, ExSale>(_saleHandler.GetItemById(value)));
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult EditSale(ExSale sale)
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCustomer(string id)
        {
            int value;
            int.TryParse(id, out value);
            var customers =
                Mapper.Map<IEnumerable<Customer>, IEnumerable<Models.ManagerSalesModels.Customer>>(
                    _customerHandler.GetList(x => true)).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

            ViewBag.Customers = customers;

            return View("Edit/EditCustomer",
                Mapper.Map<Customer, Models.ManagerSalesModels.Customer>(_customerHandler.GetItemById(value)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCustomer(Models.ManagerSalesModels.Customer customer)
        {
            try
            {
                _customerHandler.Update(Mapper.Map<Models.ManagerSalesModels.Customer, Customer>(customer));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Edit/EditCustomer", new Models.ManagerSalesModels.Customer());
            }
            return RedirectToAction("Customers", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditManager(string id)
        {
            int value;
            int.TryParse(id, out value);
            var managers =
                Mapper.Map<IEnumerable<Manager>, IEnumerable<Models.ManagerSalesModels.Manager>>(
                    _managerHandler.GetList(x => true)).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.LastName
                    });
            ViewBag.Managers = managers;

            return View("Edit/EditManager",
                Mapper.Map<Manager, Models.ManagerSalesModels.Manager>(_managerHandler.GetItemById(value)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditManager(Models.ManagerSalesModels.Manager manager)
        {
            try
            {
                _managerHandler.Update(Mapper.Map<Models.ManagerSalesModels.Manager, Manager>(manager));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Edit/EditManager", new Models.ManagerSalesModels.Manager());
            }
            return RedirectToAction("Managers", "Home");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveManager(string id)
        {
            int value;
            int.TryParse(id, out value);
            _managerHandler.Remove(_managerHandler.GetItemById(value));

            return RedirectToAction("Managers", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveCustomer(string id)
        {
            int value;
            int.TryParse(id, out value);
            _customerHandler.Remove(_customerHandler.GetItemById(value));

            return RedirectToAction("Customers", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveSale(string id)
        {
            int value;
            int.TryParse(id, out value);

            _saleHandler.Remove(_saleHandler.GetItemById(value));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveProduct(string id)
        {
            int value;
            int.TryParse(id, out value);

            _productHandler.Remove(_productHandler.GetItemById(value));

            return RedirectToAction("Products", "Home");
        }
    }
}