using System;
using AutoMapper;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;
using ManagerSales.BL.ModelsHandlers.AbstractClasses;

namespace ManagerSales.BL.ModelsHandlers
{
    public class ManagerModelHandler : ModelHandler<Models.Manager, Manager>
    {
        protected override IGenericDataRepository<Manager> Repository { get; } = new ManagerRepository();

        public ManagerModelHandler()
        {
            Mapper.CreateMap<Models.Manager, Manager>();
            Mapper.CreateMap<Manager, Models.Manager>();
        }

        public override void Add(Models.Manager item)
        {
            if (item.LastName == null) throw new ArgumentNullException();

            if (!IsExist(BlToDalModel(item), Repository))
            {
                base.Add(item);
            }
        }

        protected override Models.Manager DalToBlModel(Manager item)
        {
            return Mapper.Map<Manager, Models.Manager>(item);
        }

        protected override Manager BlToDalModel(Models.Manager item)
        {
            return Mapper.Map<Models.Manager, Manager>(item);

        }

        public override bool IsExist(Manager item, IGenericDataRepository<Manager> repository)
        {
            Manager resulIitem;

            lock (repository)
            {
                resulIitem = repository.GetSingle(item);
            }

            return resulIitem != null;
        }

    }
}