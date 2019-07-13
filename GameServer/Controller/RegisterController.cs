using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.DAO;
using GameServer.Model;

namespace GameServer.Controller
{
    class RegisterController:BaseController
    {
        UserDAO dao = new UserDAO();
        public RegisterController()
        {
            _RequestCode = RequestCode.RegisterRequest;
        }
        public string RegisterAction(string data, Server server, Client client)
        {
            string[] str = data.Split(',');
            if (dao.UserName(client.mySqlCon, str[0]))
            {
                Console.WriteLine("5");
                return  ((int)ReturnEnum.Fail).ToString();
            }
            dao.Register(client.mySqlCon, str[0], str[1]);
            return ((int)ReturnEnum.Success).ToString();
        }
    }
}
