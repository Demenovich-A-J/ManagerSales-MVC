using System;
using System.Collections.Generic;
using System.Linq;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Manager = DAL.ManagerSalesModel.Manager;

namespace DAL.ReposytoryModel
{
    public class ManagerRepository : GenericDataRepitory<ManagerSalesModel.Manager,EntytiModel.Manager>
    {
        protected override EntytiModel.Manager ObjectToEntity(ManagerSalesModel.Manager item)
        {
            return new EntytiModel.Manager
            {
                LastName = item.Name
            };
        }

        protected override ManagerSalesModel.Manager EntityToObject(EntytiModel.Manager item)
        {
            return new ManagerSalesModel.Manager(item.LastName)
            {
                Id = item.Id
            };
        }

        public override Manager GetSingle(Manager item1)
        {
            Manager item;
            using (var context = new ManagerSaleDBEntities())
            {
                item = context
                    .Set<EntytiModel.Manager>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.Name == item1.Name);
            }
            return item;
        }
    }
}