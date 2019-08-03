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
    class UserController:BaseController
    {
        UserDAO dao = new UserDAO();
        public UserController()
        {
            _RequestCode = RequestCode.User;
        }
        public string Login(string data, Server server, Client client)
        {
            string[] str = data.Split(',');
            User user= dao.VerifyUser(client.mySqlCon, str[0], str[1]);
            if (user != null)
            {
                client.SetUserData(user);
                return string.Format("{0},{1},{2},{3}", (int)ReturnEnum.Success, user.Name, user.Sumcount, user.WinCount);
                //return (int)ReturnEnum.Success + "," + user.Name + "," + user.Sumcount + "," + user.WinCount;
            }
            else
            {
                return ((int)ReturnEnum.Fail).ToString();
            }
        }
        public string Register(string data, Server server, Client client)
        {
            string[] str = data.Split(',');
            if (dao.UserName(client.mySqlCon, str[0]))
            {
                return ((int)ReturnEnum.Fail).ToString();
            }
            dao.Register(client.mySqlCon, str[0], str[1]);
            return ((int)ReturnEnum.Success).ToString();
        }
    }
}
