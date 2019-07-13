using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySqlConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            string mctStr = "database=game_player;data source=127.0.0.1;port=3306;user=root;password=763980";
            MySqlConnection mct = new MySqlConnection(mctStr);

            mct.Open();

            #region Found
            //MySqlCommand cmd = new MySqlCommand("select * from account",mct);

            //MySqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    string name = reader.GetString("account_name");
            //    string password = reader.GetString("account_password");
            //    Console.Write(name + ":" + password);
            //    Console.WriteLine();
            //}
            //reader.Close();
            #endregion

            #region Insert
            //string name = "test1";
            //string password = "123456";
            //MySqlCommand cmd = new MySqlCommand("insert into user set username=@un,userpassword=@pwd",mct);
            //cmd.Parameters.AddWithValue("un", name);
            //cmd.Parameters.AddWithValue("pwd", password);

            //cmd.ExecuteNonQuery();
            #endregion

            #region Delete
            //MySqlCommand cmd = new MySqlCommand("delete from user where userid=@id", mct);
            //cmd.Parameters.AddWithValue("id", 2);
            //cmd.ExecuteNonQuery();
            #endregion

            #region Revise
            //string pwd = "123";
            //MySqlCommand cmd = new MySqlCommand("update user set password=@pwd where userid=1", mct);
            //cmd.Parameters.AddWithValue("pwd", pwd);
            //cmd.ExecuteNonQuery();
            #endregion

            mct.Close();

            Console.ReadKey();
        }
    }
}
