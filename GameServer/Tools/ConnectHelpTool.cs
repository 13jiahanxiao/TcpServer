using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tools
{
    class ConnectHelpTool
    {
        public const string CONNECTSTRING = "database=game_player;data source=127.0.0.1;port=3306;user=root;pwd=763980";

        public static MySqlConnection Connect()
        {
            MySqlConnection myConnect = new MySqlConnection(CONNECTSTRING);
            try
            {
                myConnect.Open();
                return myConnect;
            }
            catch (Exception e)
            {
                Console.WriteLine("database wrong" + e);
                return null;
            }
        }

        public static void CloseConnect(MySqlConnection myCon)
        {
            if (myCon != null)
            {
                myCon.Close();
            }
            else Console.WriteLine("MySqlConnect is null can't close");
        }
    }
}
