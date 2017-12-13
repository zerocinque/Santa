using Santa.Classes;
using System.Collections.Generic;

namespace Santa.Models
{
    public class Users
    {
        public Users(List<User> entityList)
        {
            EntityList = entityList;
        }

        public List<User> EntityList { get; private set; }

    }
}