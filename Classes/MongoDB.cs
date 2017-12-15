using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Santa.Classes
{
    public class MongoDB : IDataBase
    {
        private IMongoDatabase database
        {
            get
            {
                return MongoConnection.Instance.Database;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            IMongoCollection<Order> orderCollection = database.GetCollection<Order>("orders");
            List<Order> orders = orderCollection.Find(new BsonDocument()).ToList();
            foreach(Order order in orders)
            {
                List<Toy> distinctToys = new List<Toy>();
                foreach(Toy originalToy in order.Toys)
                {
                    bool flag = true;
                    foreach(Toy toy in distinctToys)
                    {
                        if (toy.Name.Equals(originalToy.Name))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        distinctToys.Add(originalToy);
                }
                order.Toys = distinctToys;
            }
            return orders;
        }

        public IEnumerable<Toy> GetAllToys()
        {
            IMongoCollection<Toy> toyCollection = database.GetCollection<Toy>("toys");
            return toyCollection.Find(new BsonDocument()).ToList();
        }

        public Order GetOrder(string id)
        {
            isValidID(id);

            IMongoCollection<Order> orderCollection = database.GetCollection<Order>("orders");
            return orderCollection.Find(_ => _.ID == id).FirstOrDefault();
        }

        public Toy GetToy(string id)
        {
            isValidID(id);

            IMongoCollection<Toy> toyCollection = database.GetCollection<Toy>("toys");
            return toyCollection.Find(_ => _.ID == id).FirstOrDefault();
        }

        public User GetUser(string email, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException();

            if (password.Length != 128)
                throw new ArgumentException();

            Regex regex = new Regex("/[ ]+/");
            if (regex.Match(password).Success == true)
                throw new ArgumentException();


            IMongoCollection<User> userCollection = database.GetCollection<User>("users");
            return userCollection.Find(_ => _.Email == email && _.Password == password).FirstOrDefault();
        }

        public bool UpdateOrder(Order order)
        {
            isValidID(order.ID);

            IMongoCollection<Order> orderCollection = database.GetCollection<Order>("orders");
            var filter = Builders<Order>.Filter.Eq("_id", ObjectId.Parse(order.ID));
            var update = Builders<Order>.Update.Set("status", order.Status);
            try
            {
                orderCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void isValidID(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException();

            if (id.Length != 24)
                throw new ArgumentException();

            Regex regex = new Regex("/[ ]+/");
            if (regex.Match(id).Success == true)
                throw new ArgumentException();
        }

    }
}
