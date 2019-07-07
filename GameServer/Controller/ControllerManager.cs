using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        public Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();

        Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        public void InitController()
        {
            DefaultController controller = new DefaultController();
            controllerDict.Add(controller.Request, controller);
            controllerDict.Add(RequestCode.LoginRequest, new LoginController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                return;
            }
            string methodName = Enum.GetName(typeof(Action), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("无法得到对应的controller");
                return;
            }
            object[] parameter = new object[] { data };
            object o = mi.Invoke(controller, parameter);
            if (o == null || string.IsNullOrEmpty(o as string))
            {
                Console.WriteLine("无法得到对应的方法");
                return;
            }
            server.SendResponse(client, actionCode, o as string);
        }
    }
}
