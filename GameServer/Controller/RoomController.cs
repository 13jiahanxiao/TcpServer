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
    class RoomController:BaseController
    {
        public RoomController()
        {
            _RequestCode = RequestCode.Room;
        }
        public string CreateRoom(string data, Server server, Client client)
        {
            server.CreateRoom(client);
            return ((int)(ReturnEnum.Success)).ToString();
        }
        public string ReadRoomList(string data, Server server, Client client)
        {
            StringBuilder sb = new StringBuilder();
            foreach(Room  room in server.RoomList)
            {
                sb.Append(room.UserName() + "|");
            }
            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
