using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Servers;
namespace GameServer.Model
{
    enum RoomStatus
    {
        Wait = 0,
        Full,
        Playing,
        End
    }
    class Room
    { 
        List<Client> playerClient= new List<Client>();
        RoomStatus status = RoomStatus.Wait;
        public void SetRoomInfo(Client client)
        {
            playerClient.Add(client);
        }
        public string UserData()
        {
            return playerClient[0].user.Name+","+ playerClient[0].user.Sumcount+"," +playerClient[0].user.WinCount;
        }
        public string UserName()
        {
            return playerClient[0].user.Name;
        }
    }
}
