using AutoMapper;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;
using ManagerSales.BL.ModelsHandlers.AbstractClasses;

namespace ManagerSales.BL.ModelsHandlers
{
    public class CustomerModelHandler : ModelHandler<Models.Customer, Customer>
    {
        protected override IGenericDataRepository<Customer> Repository { get; } = new CustomerRepository();

        public CustomerModelHandler()
        {
            Mapper.CreateMap<Models.Customer, Customer>();
            Mapper.CreateMap<Customer, Models.Customer>();
        }

        protected override Models.Customer DalToBlModel(Customer item)
        {
            return Mapper.Map<Customer, Models.Customer>(item);

        }

        protected override Customer BlToDalModel(Models.Customer item)
        {
            return Mapper.Map<Models.Customer, Customer>(item);
        }
    }
}