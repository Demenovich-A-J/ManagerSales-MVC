﻿using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Product = DAL.ManagerSalesModel.Product;

namespace DAL.ReposytoryModel
{
    public class ProductRepository : GenericDataRepitory<Product, EntytiModel.Product>
    {
        public ProductRepository()
        {
            Mapper.CreateMap<Product, EntytiModel.Product>();
            Mapper.CreateMap<EntytiModel.Product, Product>();

        }

        public override void Remove(Product item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Sale.RemoveRange(context.Sale.Where(x => x.ProductId == item.Id));


                context.Entry(ObjectToEntity(item)).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        protected override EntytiModel.Product ObjectToEntity(Product item)
        {
            return Mapper.Map<EntytiModel.Product>(item);
        }

        protected override Product EntityToObject(EntytiModel.Product item)
        {
            return Mapper.Map<Product>(item);
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