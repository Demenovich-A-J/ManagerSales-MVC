using System;

namespace ManagerSales.Web.GUI.Models.ManagerSalesModels
{
    public class Sale
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Summ { get; set; }
        public Customer Customer { get; set; }
        public Manager Manager { get; set; }
        public Product Product { get; set; }
    }
}