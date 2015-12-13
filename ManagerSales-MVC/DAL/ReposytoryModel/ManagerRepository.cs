using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Manager = DAL.ManagerSalesModel.Manager;

namespace DAL.ReposytoryModel
{
    public class ManagerRepository : GenericDataRepitory<Manager,EntytiModel.Manager>
    {
        public ManagerRepository()
        {
            Mapper.CreateMap<Manager, EntytiModel.Manager>();
            Mapper.CreateMap<EntytiModel.Manager, Manager>();
        }

        public override void Remove(Manager item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Sale.RemoveRange(context.Sale.Where(x => x.ManagerId == item.Id));
                context.DocumentInfo.RemoveRange(context.DocumentInfo.Where(x => x.ManagerId == item.Id));

                context.Entry(ObjectToEntity(item)).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        protected override EntytiModel.Manager ObjectToEntity(Manager item)
        {
            return Mapper.Map<EntytiModel.Manager>(item);
        }

        protected override ManagerSalesModel.Manager EntityToObject(EntytiModel.Manager item)
        {
            return Mapper.Map<Manager>(item);
        }

        public override Manager GetSingle(Manager item1)
        {
            Manager item;
            using (var context = new ManagerSaleDBEntities())
            {
                item = context
                    .Set<EntytiModel.Manager>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.LastName == item1.LastName);
            }
            return item;
        }
    }
}