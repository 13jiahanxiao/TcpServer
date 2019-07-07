using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Message
    {
        byte[] _Data = new byte[1024];
        public byte[] Data
        {
            get { return _Data; }
        }

        int _StartIndex = 0;
        public int StartIndex
        {
            get { return _StartIndex; }
        }

        public int RemainSize
        {
            get { return _Data.Length - _StartIndex; }
        }

        string _LastString = null;
        public string LastString
        {
            get
            {
                return _LastString;
            }
        }

        public void AddCount(int count)
        {
            _StartIndex += count;
        }

        public void ReadMessage()
        {
            while (true)
            {
                if (StartIndex <= 4) return;
                int count = BitConverter.ToInt32(_Data, 0);
                if ((StartIndex - 4) >= count)
                {
                    string s = Encoding.UTF8.GetString(_Data, 4, count);
                    Console.WriteLine(s);
                    _LastString = s;
                    Array.Copy(_Data, count + 4, _Data, 0, _StartIndex - 4 - count);
                    _StartIndex -= (count + 4);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
