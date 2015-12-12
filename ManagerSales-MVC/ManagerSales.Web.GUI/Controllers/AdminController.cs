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
    public class AdminController : Controller
    {
        private readonly IModelHandler<Sale> _saleHandler;
        private readonly IModelHandler<Manager> _managerHandler;
        private readonly IModelHandler<Product> _productHandler;
        private readonly IModelHandler<Customer> _customerHandler;

        public AdminController()
        {
            _saleHandler = new SaleModelHandler();
            _managerHandler = new ManagerModelHandler();
            _productHandler = new ProductModelHandler();
            _customerHandler = new CustomerModelHandler();

            Mapper.CreateMap<Customer, Models.ManagerSalesModels.Customer>();
            Mapper.CreateMap<Product, Models.ManagerSalesModels.Product>();
            Mapper.CreateMap<Manager, Models.ManagerSalesModels.Manager>();
            Mapper.CreateMap<Sale, Models.ManagerSalesModels.ExSale>();

            Mapper.CreateMap<Models.ManagerSalesModels.Product, Product>();


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
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddProduct", new Models.ManagerSalesModels.Product());

            }

            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddManager()
        {
            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddManager(Models.ManagerSalesModels.Product product)
        {
            try
            {
                _productHandler.Add(Mapper.Map<Models.ManagerSalesModels.Product, Product>(product));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddProduct", new Models.ManagerSalesModels.Product());

            }

            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCustomer()
        {
            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCustomer(Models.ManagerSalesModels.Product product)
        {
            try
            {
                _productHandler.Add(Mapper.Map<Models.ManagerSalesModels.Product, Product>(product));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddProduct", new Models.ManagerSalesModels.Product());

            }

            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }
        /*****************************************************************/
        //work on add sale
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSale()
        {
            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }
        /*****************************************************************/
        //work on add sale
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSale(Models.ManagerSalesModels.Product product)
        {
            try
            {
                _productHandler.Add(Mapper.Map<Models.ManagerSalesModels.Product, Product>(product));
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Add/AddProduct", new Models.ManagerSalesModels.Product());

            }

            return View("Add/AddProduct", new Models.ManagerSalesModels.Product());
        }
        /**********************************************************************/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            return View("Edit/EditUser");
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
            return View("Edit/EditProduct", Mapper.Map<Product, Models.ManagerSalesModels.Product>(_productHandler.GetItemById(value)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(Models.ManagerSalesModels.Product product)
        {
            _productHandler.Update(Mapper.Map<Models.ManagerSalesModels.Product, Product>(product));
            return View("_Layout");
        }



        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult EditSale(string id)
        {
            int value;
            int.TryParse(id, out value);
            var products = Mapper.Map < IEnumerable<Product>, IEnumerable<Models.ManagerSalesModels.Product>> (_productHandler.GetList(x => true)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var customers = Mapper.Map < IEnumerable<Customer>, IEnumerable< Models.ManagerSalesModels.Customer >>( _customerHandler.GetList(x => true)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            var managers = Mapper.Map < IEnumerable<Manager>, IEnumerable< Models.ManagerSalesModels.Manager>> (_managerHandler.GetList(x => true)).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.LastName
            });

            ViewBag.Products = products;
            ViewBag.Customers = customers;
            ViewBag.Managers = managers;



            return View("Edit/EditSale", Mapper.Map<Sale, Models.ManagerSalesModels.ExSale>(_saleHandler.GetItemById(value)));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCustomer(string id)
        {
            int value;
            int.TryParse(id, out value);
            return View("Edit/EditCustomer", Mapper.Map<Customer, Models.ManagerSalesModels.Customer>(_customerHandler.GetItemById(value)));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCustomer(Models.ManagerSalesModels.Customer customer )
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
            return View("_Layout");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditManager(string id)
        {
            int value;
            int.TryParse(id, out value);
            return View("Edit/EditProduct", Mapper.Map<Product, Models.ManagerSalesModels.Product>(_productHandler.GetItemById(value)));
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
            return View("_Layout");
        }

    }
}