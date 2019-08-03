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
        public string Name
        {
            get { return name; }
        }
        string password;
        int sumCount;
        public int Sumcount
        {
            get { return sumCount; }
        }
        int winCount;
        public int WinCount
        {
            get { return winCount; }
        }
        public User(int idValue,string nameValue,string passwordValue,int sum,int win)
        {
            id = idValue;
            name = nameValue;
            password = passwordValue;
            sumCount = sum;
            winCount = win;
        }
    }
}
