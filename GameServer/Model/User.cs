using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class User
    {
        int id;
        string name;
        string password;
        public User(int idValue,string nameValue,string passwordValue)
        {
            id = idValue;
            name = nameValue;
            password = passwordValue;
        }
    }
}
