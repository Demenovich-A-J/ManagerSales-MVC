using System;

namespace DAL.ManagerSalesModel
{
    public class DocumentInfo
    {
        private readonly string _documentName;
        private readonly DocumentInfoStatus _documentInfoStatus;
        private readonly DateTime _date;

        public DocumentInfo(string documentName, DocumentInfoStatus documentInfoStatus, DateTime date)
        {
            _documentName = documentName;
            _documentInfoStatus = documentInfoStatus;
            _date = date;
        }

        public int Id { get; set; }
        public int ManagerId { get; set; }
        public virtual Manager Manager { get; set; }

        public string DocumentName => _documentName;
        public string Status => _documentInfoStatus.ToString();
        public DateTime Date => _date;
    }
}