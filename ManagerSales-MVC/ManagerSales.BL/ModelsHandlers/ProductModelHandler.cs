using System;
using AutoMapper;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;
using ManagerSales.BL.ModelsHandlers.AbstractClasses;

namespace ManagerSales.BL.ModelsHandlers
{
    public class ProductModelHandler : ModelHandler<Models.Product, Product>
    {
        protected override IGenericDataRepository<Product> Repository { get; } = new ProductRepository();

        public ProductModelHandler()
        {
            Mapper.CreateMap<Product, Models.Product>();
            Mapper.CreateMap<Models.Product, Product>();
        }

        public override void Add(Models.Product item)
        {
            if (item.Name == null) throw new ArgumentNullException();

            if (!IsExist(BlToDalModel(item),Repository))
            {
                base.Add(item);
            }
        }

        protected override Models.Product DalToBlModel(Product item)
        {
            return Mapper.Map<Product, Models.Product>(item);
        }

        protected override Product BlToDalModel(Models.Product item)
        {
            return Mapper.Map<Models.Product, Product>(item);
        }

        public override bool IsExist(Product item, IGenericDataRepository<Product> repository)
        {
            Product resulIitem;

            lock (repository)
            {
                resulIitem = repository.GetSingle(item);
            }

            return resulIitem != null;
        }

    }
}