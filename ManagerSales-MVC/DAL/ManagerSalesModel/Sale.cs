using System;

namespace DAL.ManagerSalesModel
{
    public sealed class Sale
    {
        public DateTime Date { get; set; }
        public double Summ { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ManagerId { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public Manager Manager { get; set; }
    }
}