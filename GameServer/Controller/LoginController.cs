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
    class LoginController:BaseController
    {
        UserDAO dao = new UserDAO();
        public LoginController()
        {
            _RequestCode = RequestCode.LoginRequest;
        }
        public string Login(string data, Server server, Client client)
        {
            string[] str = data.Split(',');
            User user= dao.VerifyUser(client.mySqlCon, str[0], str[1]);
            if (user != null)
            {
                return ((int)ReturnEnum.Success).ToString();
            }
            else
            {
                return ((int)ReturnEnum.Fail).ToString();
            }
        }
    }
}
