using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    class MessageReceive
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

        public void ReadMessage(int Amount,Action<RequestCode,ActionCode,string> processMassageCallback)
        {
            _StartIndex += Amount;
            while (true)
            {
                if (StartIndex <= 4) return;
                int count = BitConverter.ToInt32(_Data, 0);
                if ((StartIndex - 4) >= count)
                {
                    //string s = Encoding.UTF8.GetString(_Data, 4, count);
                    //Console.WriteLine(s);
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(Data, 4);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(Data, 8);
                    string s = Encoding.UTF8.GetString(Data, 12, count - 8);
                    _LastString = s;
                    processMassageCallback(requestCode, actionCode, s);
                    Array.Copy(_Data, count + 12, _Data, 0, _StartIndex - 12 - count);
                    _StartIndex -= (count + 4);
                }
                else
                {
                    return;
                }
            }
        }

        public static byte[] PackData(ActionCode actionCode,string data)
        {
            byte[] actionBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] amount = BitConverter.GetBytes( (int)actionBytes.Length + dataBytes.Length);
            amount.Concat(actionBytes).Concat(dataBytes);
            return amount;
        }
    }
}
