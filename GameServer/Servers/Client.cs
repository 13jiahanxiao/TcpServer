using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tools;

namespace GameServer.Servers
{
    class Client
    {
        Socket clientSocket;
        Server server;
        MessageReceive msg = new MessageReceive();
        MySqlConnection mySqlConnect;
        public MySqlConnection mySqlCon
        {
            get { return mySqlConnect; }
        }

        Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mySqlConnect = ConnectHelpTool.Connect();
        }

        public void Start()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }

        void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count,ProcessMassage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }
        void Close()
        {
            ConnectHelpTool.CloseConnect(mySqlConnect);
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            server.RemoveClient(this);
        }

        void ProcessMassage(RequestCode requestCode,ActionCode actionCode,string s)
        {
            server.HandleRequset(requestCode, actionCode, s, this);
        }

        public void SendMessage(ActionCode actionCode ,string data)
        {
            byte[] message = MessageReceive.PackData(actionCode, data);
            clientSocket.Send(message);
        }
    }
}
