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
        public ActionResult Index()
        {
            IModelHandler<Sale> h = new SaleModelHandler();
            var d = h.GetList(x => true);
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
            ICollection<Models.Sale> s = d.Select(Mapper.Map<Sale, Models.Sale>).ToList();

            return View("SalesGrid/SaleGrid",s);
        }
    }
}