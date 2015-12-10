using System;
using System.Linq;
using AutoMapper;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using DocumentInfo = DAL.ManagerSalesModel.DocumentInfo;
using Manager = DAL.ManagerSalesModel.Manager;
using Sale = DAL.ManagerSalesModel.Sale;

namespace DAL.ReposytoryModel
{
    public class DocumentInfoRepository : GenericDataRepitory<DocumentInfo,EntytiModel.DocumentInfo>
    {
        public DocumentInfoRepository()
        {
            Mapper.CreateMap<DocumentInfo, EntytiModel.DocumentInfo>();
            Mapper.CreateMap<DocumentInfo, EntytiModel.DocumentInfo>();
        }

        protected override EntytiModel.DocumentInfo ObjectToEntity(DocumentInfo item)
        {
            return Mapper.Map<EntytiModel.DocumentInfo>(item);
        }

        protected override DocumentInfo EntityToObject(EntytiModel.DocumentInfo item)
        {
            return Mapper.Map<DocumentInfo>(item);
        }

        public override DocumentInfo GetSingle(DocumentInfo item)
        {
            DocumentInfo resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.DocumentInfo>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.DocumentName == item.DocumentName);
            }
            return resItem;
        }
    }
}