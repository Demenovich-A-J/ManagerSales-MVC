using System;

namespace ManagerSales.Web.GUI.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string ManagerName { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public double Summ { get; set; }
    }
}