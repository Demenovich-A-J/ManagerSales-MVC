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
        protected readonly IModelHandler<Sale> _saleHandler;
        protected IModelHandler<Manager> _managerHandler; 
        public ManagerSalesController()
        {
            _saleHandler = new SaleModelHandler();
            _managerHandler = new ManagerModelHandler();
        }

        public ActionResult Index()
        {
            var sales = _saleHandler.GetList(x => true);
            Mapper.CreateMap<Sale, Models.Sale>()
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

            return View("SalesGrid/SaleGrid", sales.Select(Mapper.Map<Sale, Models.Sale>).ToList());
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