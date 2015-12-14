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
    public class CustomerController : Controller
    {
        private readonly IModelHandler<Customer> _customerHandler;

        public CustomerController()
        {
            _customerHandler = new CustomerModelHandler();
            Mapper.CreateMap<Customer, Models.ManagerSalesModels.Customer>();
            Mapper.CreateMap<Models.ManagerSalesModels.Customer, Customer>();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Customers",
                _customerHandler.GetAll().Select(Mapper.Map<Customer, Models.ManagerSalesModels.Customer>).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CustomerGrid()
        {
            return View("CustomerGrid", _customerHandler.GetAll().Select(Mapper.Map<Customer, Models.ManagerSalesModels.Customer>).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCustomer()
        {
            return View("AddCustomer", new Models.ManagerSalesModels.Customer());
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
                return View("AddCustomer", new Models.ManagerSalesModels.Customer());
            }

            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCustomer(string id)
        {
            int value;
            int.TryParse(id, out value);
            var customers =
                Mapper.Map<IEnumerable<Customer>, IEnumerable<Models.ManagerSalesModels.Customer>>(
                    _customerHandler.GetAll()).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

            ViewBag.Customers = customers;

            return View("EditCustomer",
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
                return View("EditCustomer", new Models.ManagerSalesModels.Customer());
            }
            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveCustomer(string id)
        {
            int value;
            int.TryParse(id, out value);
            _customerHandler.Remove(_customerHandler.GetItemById(value));

            return RedirectToAction("Index", "Customer");
        }
    }
}