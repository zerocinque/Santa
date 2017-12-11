using System.Collections.Generic;

namespace Santa.Classes
{
    public interface IDataBase
    {
        bool UpdateOrder(Order order);

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Toy> GetAllToys();

        Order GetOrder(string id);
        Toy GetToy(string id);
        User GetUser(User user);
    }
}
