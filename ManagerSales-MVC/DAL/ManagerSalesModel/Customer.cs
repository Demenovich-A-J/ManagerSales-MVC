namespace DAL.ManagerSalesModel
{
    public class Customer
    {
        private readonly string _lastName;
        public Customer(string lastName)
        {
            _lastName = lastName;
        }

        public string LastName => _lastName;

        public int Id { get; set; }
    }
}