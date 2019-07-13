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
        public User VerifyUser(MySqlConnection mysqlCon, string name, string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from account where account_name=@name and account_password=@password", mysqlCon);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("Account_Id");
                    User user = new User(id, name, password);
                    return user;
                }
                else return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("验证出现错误！" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;

        }
        public bool UserName(MySqlConnection mysqlCon, string name)
        {
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand("select * from account where account_name=@name", mysqlCon);
            cmd.Parameters.AddWithValue("name", name);
            reader = cmd.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("查找出现错误" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return true;
        }
        public void Register(MySqlConnection mysqlCon, string name, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into account set account_name = @name , account_password = @password", mysqlCon);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("新建信息出现错误" + e);
            }
        }
    }
}
