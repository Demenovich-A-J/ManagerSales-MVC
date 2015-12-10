using System.Linq;
using AutoMapper;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Customer = DAL.ManagerSalesModel.Customer;

namespace DAL.ReposytoryModel
{
    public class CustomerRepository : GenericDataRepitory<Customer, EntytiModel.Customer>
    {
        public CustomerRepository()
        {
            Mapper.CreateMap<Customer, EntytiModel.Customer>();
            Mapper.CreateMap<EntytiModel.Customer, Customer>();
        }

        protected override EntytiModel.Customer ObjectToEntity(Customer item)
        {
            return Mapper.Map<EntytiModel.Customer>(item);
        }

        protected override ManagerSalesModel.Customer EntityToObject(EntytiModel.Customer item)
        {
            return Mapper.Map<Customer>(item);
        }

        public override Customer GetSingle(Customer item)
        {
            Customer resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.Customer>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.Name == item.Name);
            }
            return resItem;
        }
    }
}