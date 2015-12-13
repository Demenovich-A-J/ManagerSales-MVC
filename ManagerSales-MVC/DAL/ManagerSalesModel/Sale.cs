using System;

namespace DAL.ManagerSalesModel
{
    public class Sale
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double Summ { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
    }
}