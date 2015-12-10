namespace ManagerSales.BL.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public System.DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public double Summ { get; set; }

        public virtual ManagerSales.BL.Models.Customer Customer { get; set; }
        public virtual ManagerSales.BL.Models.Manager Manager { get; set; }
        public virtual ManagerSales.BL.Models.Product Product { get; set; }
    }
}