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
                    int sum, win;
                    reader.Close();
                    if (GetResult(mysqlCon, id, out sum, out win))
                    {
                        Console.WriteLine(1);
                        User user = new User(id, name, password, sum, win);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
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
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into account set account_name = @name , account_password = @password", mysqlCon);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();

                MySqlCommand cmd3 = new MySqlCommand("select * from account where account_name=@name", mysqlCon);
                cmd3.Parameters.AddWithValue("name", name);
                reader = cmd3.ExecuteReader();
                int id = 0;
                if (reader.Read())
                {
                    id = reader.GetInt32("Account_Id");
                }
                reader.Close();

                MySqlCommand cmd2 = new MySqlCommand("insert int result set userid=@userid,TotalCount=@total,WinCount=@win", mysqlCon);
                cmd2.Parameters.AddWithValue("userid", id);
                cmd2.Parameters.AddWithValue("total", 0);
                cmd2.Parameters.AddWithValue("win", 0);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("新建信息出现错误" + e);
            }
        }
        public bool GetResult(MySqlConnection mysqlCon,int id,out int sum,out int win)
        {
            MySqlCommand cmd = new MySqlCommand("select * from result where userid=@id", mysqlCon);
            cmd.Parameters.AddWithValue("id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                sum = reader.GetInt32("TotalCount");
                win = reader.GetInt32("WinCount");
                Console.WriteLine(2);
                reader.Close();
                return true;
            }
            else
            {
                sum = 0;
                win = 0;
                Console.WriteLine(3);
                reader.Close();
                return false;
            }
        }
    }
}
