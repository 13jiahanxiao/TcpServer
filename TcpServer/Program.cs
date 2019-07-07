using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TcpServer
{
    class Program
    {
        //static byte[] msgBuff = new byte[1024];
        static Message msgBuff = new Message();
        static void Main(string[] args)
        {
            StartServer();
            Console.ReadKey();
        }

        static void StartServer()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("192.168.1.102");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 88);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(10);
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);

            //Socket clientSocket = serverSocket.Accept();
            //同步开启
            //byte[] msgBuff = new byte[1024];
            //int count = clientSocket.Receive(msgBuff);
            //string msgReceive = System.Text.Encoding.UTF8.GetString(msgBuff, 0, count);
            //Console.WriteLine(msgReceive);
            //clientSocket.Close();
            //serverSocket.Close();
        }

        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);

            string msg = "hello,你好";
            byte[] date = System.Text.Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(date);

            clientSocket.BeginReceive(msgBuff.Data, msgBuff.StartIndex, msgBuff.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            if (msgBuff.LastString == "close")
            {
                serverSocket.Close();
            }
            else
            {
                serverSocket.BeginAccept(AcceptCallBack, serverSocket);
            }
        }

        static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);

                msgBuff.AddCount(count);

                msgBuff.ReadMessage();

                clientSocket.BeginReceive(msgBuff.Data, msgBuff.StartIndex, msgBuff.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);

                //string msg = Encoding.UTF8.GetString(msgBuff.Data, 0, count);
                //if (msg == "Close")
                //{
                //    clientSocket.Close();
                //    Console.WriteLine("Close");
                //    return;
                //}
                //Console.WriteLine("收到:" + msg);
                //clientSocket.BeginReceive(msgBuff, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception e)
            {
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
        }
    }
}
