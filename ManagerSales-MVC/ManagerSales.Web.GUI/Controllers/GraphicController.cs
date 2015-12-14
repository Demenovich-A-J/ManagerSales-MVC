using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ManagerSales.BL.Models;
using ManagerSales.BL.ModelsHandlers;
using ManagerSales.BL.ModelsHandlers.Intarfaces;

namespace ManagerSales.Web.GUI.Controllers
{
    public class GraphicController : Controller
    {
        private readonly IModelHandler<Manager> _managerHandler;
        private readonly IModelHandler<Product> _productHandler;
        private readonly IModelHandler<Sale> _saleHandler;

        public GraphicController()
        {
            _saleHandler = new SaleModelHandler();
            _managerHandler = new ManagerModelHandler();
            _productHandler = new ProductModelHandler();

            Mapper.CreateMap<Customer, Models.ManagerSalesModels.Customer>();
            Mapper.CreateMap<Product, Models.ManagerSalesModels.Product>();
            Mapper.CreateMap<Manager, Models.ManagerSalesModels.Manager>();
            Mapper.CreateMap<Sale, Models.ManagerSalesModels.Sale>();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetJsonForProductGraphic()
        {
            var sales =
                Mapper.Map<IEnumerable<Sale>, IEnumerable<Models.ManagerSalesModels.Sale>>(
                    _saleHandler.GetList(x => true));

            var products =
                Mapper.Map<IEnumerable<Product>, IEnumerable<Models.ManagerSalesModels.Product>>(
                    _productHandler.GetList(x => true));

            var dict = new
            {
                name = "Products",
                colorByPoint = true,
                data = products
                    .Select(x => new {name = x.Name, y = sales.Count(y => y.Product.Id == x.Id)})
                    .ToArray()
            };

            var array = new object[] {dict};

            return Json(array, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetJsonForManagerGraphic()
        {
            var sales =
                Mapper.Map<IEnumerable<Sale>, IEnumerable<Models.ManagerSalesModels.Sale>>(
                    _saleHandler.GetList(x => true));

            var salesNumber = (double) sales.Count()/100;

            var managers =
                Mapper.Map<IEnumerable<Manager>, IEnumerable<Models.ManagerSalesModels.Manager>>(
                    _managerHandler.GetList(x => true));

            var dict = new
            {
                name = "Managers",
                colorByPoint = true,
                data = managers
                    .Select(
                        x =>
                            new
                            {
                                name = x.LastName,
                                y = salesNumber != 0 ? (double) sales.Count(y => y.Manager.Id == x.Id)/salesNumber : 0,
                                drilldown = x.LastName
                            })
                    .ToArray()
            };

            var array = new object[] {dict};

            return Json(array, JsonRequestBehavior.AllowGet);
        }
    }
}