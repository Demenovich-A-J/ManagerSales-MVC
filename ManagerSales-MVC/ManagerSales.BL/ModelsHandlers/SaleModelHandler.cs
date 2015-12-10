using AutoMapper;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;
using ManagerSales.BL.ModelsHandlers.AbstractClasses;

namespace ManagerSales.BL.ModelsHandlers
{
    public class SaleModelHandler : ModelHandler<Models.Sale, Sale>
    {
        public SaleModelHandler()
        {
            Mapper.CreateMap<Models.Sale, Sale>();
            Mapper.CreateMap<Sale, Models.Sale>();

            Mapper.CreateMap<Customer, Models.Customer>();
            Mapper.CreateMap<Product, Models.Product>();
            Mapper.CreateMap<Manager, Models.Manager>();
            Mapper.CreateMap<Models.Customer, Customer>();
            Mapper.CreateMap<Models.Product, Product>();
            Mapper.CreateMap<Models.Manager, Manager>();

        }

        protected override IGenericDataRepository<Sale> Repository { get; } = new SaleRepository();

        protected override Models.Sale DalToBlModel(Sale item)
        {
            return Mapper.Map<Sale, Models.Sale>(item);
        }

        protected override Sale BlToDalModel(Models.Sale item)
        {
            return Mapper.Map<Models.Sale, Sale>(item);
        }
    }
}