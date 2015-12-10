using AutoMapper;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;
using ManagerSales.BL.Models;
using ManagerSales.BL.ModelsHandlers.AbstractClasses;

namespace ManagerSales.BL.ModelsHandlers
{
    public class ManagerModelHandler : ModelHandler<Manager,DAL.ManagerSalesModel.Manager>
    {
        protected override IGenericDataRepository<DAL.ManagerSalesModel.Manager> Repository { get; } = new ManagerRepository();
        protected override Manager DalToBlModel(DAL.ManagerSalesModel.Manager item)
        {
            return Mapper.Map<Manager>(item);
        }

        protected override DAL.ManagerSalesModel.Manager BlToDalModel(Manager item)
        {
            return Mapper.Map<DAL.ManagerSalesModel.Manager>(item);
        }
    }
}