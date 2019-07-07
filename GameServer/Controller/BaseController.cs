using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    abstract class BaseController
    {
        protected RequestCode _RequestCode = RequestCode.None;

        public RequestCode Request
        {
            get
            {
                return _RequestCode;
            }
        }
        public virtual string DefaultHandle(string data, Server server, Client client)
        {
            return data;
        }
    }
}
