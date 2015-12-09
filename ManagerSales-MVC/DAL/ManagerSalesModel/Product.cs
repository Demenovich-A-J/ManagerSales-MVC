namespace DAL.ManagerSalesModel
{
    public class Product
    {
        private readonly string _name;

        public Product(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public int Id { get; set; }
    }
}