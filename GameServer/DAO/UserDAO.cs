using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {
        public User VerifyUser(MySqlConnection mysqlCon,string name,string password)
        {
            MySqlCommand cmd = new MySqlCommand("select * form account where account_name = @name and account_password = @password", mysqlCon);
            cmd.Parameters.AddWithValue("name" ,name);
            cmd.Parameters.AddWithValue("password", password);
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    int id = reader.GetInt32("account_id");
                    User user = new User(id, name, password);
                    return user;
                }
                else return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("验证出现错误！"+e);
                return null;
            }
            
        }


    }
}
