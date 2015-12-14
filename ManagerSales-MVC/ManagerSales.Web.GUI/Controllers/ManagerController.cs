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
    public class ManagerController : Controller
    {
        private readonly IModelHandler<Manager> _managerHandler;

        public ManagerController()
        {
            _managerHandler = new ManagerModelHandler();
            Mapper.CreateMap<Manager, Models.ManagerSalesModels.Manager>();
            Mapper.CreateMap<Models.ManagerSalesModels.Manager, Manager>();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Managers",
                _managerHandler.GetList(x => true).Select(Mapper.Map<Manager, Models.ManagerSalesModels.Manager>).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ManagerGrid()
        {
            return View("ManagerGrid", _managerHandler.GetList(x => true).Select(Mapper.Map<Manager, Models.ManagerSalesModels.Manager>).ToList());
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddManager()
        {
            return View("AddManager", new Models.ManagerSalesModels.Manager());
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
                return View("AddManager", new Models.ManagerSalesModels.Manager());
            }

            return RedirectToAction("Index", "Manager");
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

            return View("EditManager",
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
                return View("EditManager", new Models.ManagerSalesModels.Manager());
            }
            return RedirectToAction("Index", "Manager");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveManager(string id)
        {
            int value;
            int.TryParse(id, out value);
            _managerHandler.Remove(_managerHandler.GetItemById(value));

            return RedirectToAction("Index", "Manager");
        }
    }
}