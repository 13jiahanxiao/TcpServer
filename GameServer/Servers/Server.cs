using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Common;
using GameServer.Controller;
using GameServer.Model;

namespace GameServer.Servers
{
    class Server
    {
        IPEndPoint ipEndPoint;
        Socket serverSocket;
        List<Client> clientList;
        List<Room> roomList;
        public List<Room> RoomList
        {
            get { return roomList; }
        }
        ControllerManager controllerManager;

        Server()
        {

        }

        public Server(string ipAddress, int port)
        {
            controllerManager = new ControllerManager(this);
            SetIpEndPoint(ipAddress, port);
        }

        public void SetIpEndPoint(string ipAddress, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }//ip地址和端口号

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            clientList = new List<Client>();
            roomList = new List<Room>();
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        void AcceptCallBack(IAsyncResult ar)
        {
            Socket ClientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(ClientSocket, this);
            client.Start();
            clientList.Add(client);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }

        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.SendMessage(actionCode, data);
        }

        public void HandleRequset(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client,this);
        }

        public void CreateRoom(Client client)
        {
            Room room = new Room();
            room.SetRoomInfo(client);
            roomList.Add(room);
        }
    }
}
