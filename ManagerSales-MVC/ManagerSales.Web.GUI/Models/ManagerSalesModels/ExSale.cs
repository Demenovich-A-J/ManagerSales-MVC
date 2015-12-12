using System;

namespace ManagerSales.Web.GUI.Models.ManagerSalesModels
{
    public class ExSale
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public double Summ { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
    }
}