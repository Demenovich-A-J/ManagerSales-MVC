using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ManagerSales.BL.Models;
using ManagerSales.BL.ModelsHandlers;
using ManagerSales.BL.ModelsHandlers.Intarfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ManagerSales.Web.GUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IModelHandler<Sale> _saleHandler;
        private readonly IModelHandler<Manager> _managerHandler;
        private readonly IModelHandler<Product> _productHandler;
        private readonly IModelHandler<Customer> _customerHandler;

        public HomeController()
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
        public ActionResult Index()
        {
            return View(_saleHandler.GetList(x => true).Select(Mapper.Map<Sale, Models.ManagerSalesModels.Sale>));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Users()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Managers()
        {
            return View(_managerHandler.GetList(x => true).Select(Mapper.Map<Manager, Models.ManagerSalesModels.Manager>));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Customers()
        {
            return View(_customerHandler.GetList(x => true).Select(Mapper.Map<Customer, Models.ManagerSalesModels.Customer>));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Products()
        {
            return View(_productHandler.GetList(x => true).Select(Mapper.Map<Product, Models.ManagerSalesModels.Product>));
        }
    }
}