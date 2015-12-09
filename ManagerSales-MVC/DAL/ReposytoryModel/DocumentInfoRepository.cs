using System;
using System.Linq;
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
        protected override EntytiModel.DocumentInfo ObjectToEntity(DocumentInfo item)
        {
            return new EntytiModel.DocumentInfo
            {
                ManagerId = item.ManagerId,
                DocumentName = item.DocumentName,
                Date = item.Date,
                Status = item.Status
            };
        }

        protected override DocumentInfo EntityToObject(EntytiModel.DocumentInfo item)
        {
            return new DocumentInfo(item.DocumentName, (DocumentInfoStatus)Enum.Parse(typeof(DocumentInfoStatus), item.Status), item.Date)
            {
                ManagerId = item.ManagerId,
                Manager = new Manager(item.Manager.LastName) {Id = item.ManagerId},
                Id = item.Id
            };
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