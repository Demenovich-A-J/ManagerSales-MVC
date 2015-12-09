using System.Linq;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Customer = DAL.ManagerSalesModel.Customer;
using Manager = DAL.ManagerSalesModel.Manager;
using Product = DAL.ManagerSalesModel.Product;
using Sale = DAL.ManagerSalesModel.Sale;

namespace DAL.ReposytoryModel
{
    public class SaleRepository : GenericDataRepitory<Sale, EntytiModel.Sale>
    {
        protected override EntytiModel.Sale ObjectToEntity(Sale item)
        {
            return new EntytiModel.Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                CustomerId = item.CustomerId,
                ManagerId = item.ManagerId,
                ProductId = item.ProductId
            };
        }

        protected override Sale EntityToObject(EntytiModel.Sale item)
        {
            return new Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                CustomerId = item.CustomerId,
                ManagerId = item.ManagerId,
                ProductId = item.ProductId,
                Customer = new Customer(item.Customer.Name),
                Manager = new Manager(item.Manager.LastName),
                Product = new Product(item.Product.Name)
            };
        }

        public override Sale GetSingle(Sale item)
        {
            Sale resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.Sale>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.Manager == item.Manager);
            }
            return resItem;
        }
    }
}