using System;

namespace DAL.ManagerSalesModel
{
    public class Manager 
    {
        private readonly string _name;
        public Manager(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public int Id { get; set; }
    }
}