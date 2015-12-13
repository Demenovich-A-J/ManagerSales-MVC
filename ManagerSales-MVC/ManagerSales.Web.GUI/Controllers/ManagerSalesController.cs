using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ManagerSales.BL.Models;
using ManagerSales.BL.ModelsHandlers;
using ManagerSales.BL.ModelsHandlers.Intarfaces;

namespace ManagerSales.Web.GUI.Controllers
{
    public class ManagerSalesController : Controller
    {
        private readonly IModelHandler<Sale> _saleHandler;
        private readonly IModelHandler<Manager> _managerHandler;
        private readonly IModelHandler<Product> _productHandler;
        private readonly IModelHandler<Customer> _customerHandler;

        public ManagerSalesController()
        {
            _saleHandler = new SaleModelHandler();
            _managerHandler = new ManagerModelHandler();
            _productHandler = new ProductModelHandler();
            _customerHandler = new CustomerModelHandler();

            Mapper.CreateMap<Customer, Models.ManagerSalesModels.Customer>();
            Mapper.CreateMap<Product, Models.ManagerSalesModels.Product>();
            Mapper.CreateMap<Manager, Models.ManagerSalesModels.Manager>();


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
        [AllowAnonymous]
        public ActionResult SaleGrid()
        {
            return View("PartialSalesGrid/SaleGrid", _saleHandler.GetList(x => true).Select(Mapper.Map<Sale, Models.ManagerSalesModels.Sale>).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ProductGrid()
        {
            return View("PartialSalesGrid/ProductGrid", _productHandler.GetList(x => true).Select(Mapper.Map<Product, Models.ManagerSalesModels.Product>).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ManagerGrid()
        {
            return View("PartialSalesGrid/ManagerGrid", _managerHandler.GetList(x => true).Select(Mapper.Map<Manager, Models.ManagerSalesModels.Manager>).ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CustomerGrid()
        {
            return View("PartialSalesGrid/CustomerGrid", _customerHandler.GetList(x => true).Select(Mapper.Map<Customer, Models.ManagerSalesModels.Customer>).ToList());
        }

        public ActionResult Edit(string id)
        {
            int id1;
            int.TryParse(id, out id1);
            var sale = _saleHandler.GetItemById(id1);
            return Redirect("/");
        }
    }
}