using ManagerSales.BL.Models;
using ManagerSales.BL.ModelsHandlers;
using ManagerSales.BL.ModelsHandlers.Intarfaces;

namespace Test.BL_Console_
{
    class Program
    {
        static void Main(string[] args)
        {
            IModelHandler<Sale> a = new SaleModelHandler();
            var d = a.GetList(x => true);
        }
    }
}
