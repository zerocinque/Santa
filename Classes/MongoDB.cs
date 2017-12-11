using Classes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Santa.Classes
{
    public class MongoDB : IDataBase
    {
        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Toy> GetAllToys()
        {
            throw new NotImplementedException();
        }

        public Order GetOrder()
        {
            throw new NotImplementedException();
        }

        public Toy GetToy()
        {
            throw new NotImplementedException();
        }

        public User GetUser()
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
