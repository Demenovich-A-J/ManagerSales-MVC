using System.Linq;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Customer = DAL.ManagerSalesModel.Customer;

namespace DAL.ReposytoryModel
{
    public class CustomerRepository : GenericDataRepitory<ManagerSalesModel.Customer, EntytiModel.Customer>
    {
        protected override EntytiModel.Customer ObjectToEntity(ManagerSalesModel.Customer item)
        {
            return new EntytiModel.Customer()
            {
                Name = item.LastName
            };
        }

        protected override ManagerSalesModel.Customer EntityToObject(EntytiModel.Customer item)
        {
            return new ManagerSalesModel.Customer(item.Name)
            {
                Id = item.Id
            };
        }

        public override Customer GetSingle(Customer item)
        {
            Customer resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.Customer>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.LastName == item.LastName);
            }
            return resItem;
        }
    }
}