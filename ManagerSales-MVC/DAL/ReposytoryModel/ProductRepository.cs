using System.Linq;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Product = DAL.ManagerSalesModel.Product;

namespace DAL.ReposytoryModel
{
    public class ProductRepository : GenericDataRepitory<ManagerSalesModel.Product, EntytiModel.Product>
    {
        protected override EntytiModel.Product ObjectToEntity(ManagerSalesModel.Product item)
        {
            return new EntytiModel.Product
            {
                Name = item.Name
            };
        }

        protected override ManagerSalesModel.Product EntityToObject(EntytiModel.Product item)
        {
            return new ManagerSalesModel.Product(item.Name)
            {
                Id = item.Id
            };
        }

        public override Product GetSingle(Product item)
        {
            Product resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.Product>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.Name == item.Name);
            }
            return resItem;
        }
    }
}