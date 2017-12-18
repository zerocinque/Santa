using System.Collections.Generic;

namespace Santa.Classes
{
    public interface IDataBase
    {
        bool UpdateOrder(Order order);
        bool UpdateAllToys(IEnumerable<Toy> toys);

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Toy> GetAllToys();

        Order GetOrder(string id);
        Toy GetToy(string id);
        Toy GetToyByName(string name);
        User GetUser(string email, string password);
    }
}
