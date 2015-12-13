using System.Linq;
using AutoMapper;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Sale = DAL.ManagerSalesModel.Sale;
using Manager = DAL.ManagerSalesModel.Manager;
using Customer = DAL.ManagerSalesModel.Customer;
using Product = DAL.ManagerSalesModel.Product;




namespace DAL.ReposytoryModel
{
    public class SaleRepository : GenericDataRepitory<Sale, EntytiModel.Sale>
    {
        public SaleRepository()
        {
            Mapper.CreateMap<Sale, EntytiModel.Sale>();
            Mapper.CreateMap<Sale, EntytiModel.Sale>()
               .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
               .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.Manager.Id))
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
               .ForMember(dest => dest.Customer, opt => opt.Ignore())
               .ForMember(dest => dest.Product, opt => opt.Ignore())
               .ForMember(dest => dest.Manager, opt => opt.Ignore());
            Mapper.CreateMap<EntytiModel.Sale, Sale>();


            Mapper.CreateMap<Manager, EntytiModel.Manager>();
            Mapper.CreateMap<Customer, EntytiModel.Customer>();
            Mapper.CreateMap<Product, EntytiModel.Product>();
            Mapper.CreateMap<EntytiModel.Manager, Manager>();
            Mapper.CreateMap<EntytiModel.Customer, Customer>();
            Mapper.CreateMap<EntytiModel.Product, Product>();

        }

        protected override EntytiModel.Sale ObjectToEntity(Sale item)
        {
            return Mapper.Map<EntytiModel.Sale>(item);
        }

        protected override Sale EntityToObject(EntytiModel.Sale item)
        {
            return Mapper.Map<EntytiModel.Sale, Sale>(item);
        }

        public override Sale GetSingle(Sale item)
        {
            Sale resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.Sale>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.Manager == item.Manager && x.Product == item.Product && x.Customer == item.Customer && x.Date == item.Date);
            }
            return resItem;
        }
    }
}