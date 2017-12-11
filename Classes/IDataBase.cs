using System.Collections.Generic;

namespace Classes
{
    public interface IDataBase
    {
        Order UpdateOrder(Order order);

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Toy> GetAllToys();

        Order GetOrder();
        Toy GetToy();
        User GetUser();
    }
}
