using MySqlConnector;
using System.Net.Http.Headers;
using System.Data.SqlClient;
using System.Data;

namespace Lab2
{
    public class Datebase
    {
        public MySqlConnection connection;
        public MySqlCommand cmd = new MySqlCommand();
        SqlDataAdapter dataAdapter;
        public Datebase()
        {

            MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=db-102;");
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS `Tabl` (" +
                "id SERIAL PRIMARY KEY, "  +
                "Score int DEFAULT 0)";
            //cmd = "CREATE TABLE IF EXISTS Score";
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "TRUNCATE `Tabl`";
            cmd.ExecuteNonQuery();

        }

        public void Insert(string columns, string lines)
        {
            cmd.CommandText = "INSERT INTO `Tabl` (" +
                columns + ") VALUES (" + lines + ")";
            cmd.ExecuteNonQuery();
        }

        public int Select(int id)
        {
            cmd.CommandText = "SELECT Score " +
                "FROM `Tabl` WHERE id = " + id;
            cmd.ExecuteNonQuery();
            int result = 0;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }
            //cmd.ExecuteNonQuery();
            return result;
        }

        public void Update(int id, int score)
        {
            cmd.CommandText = "UPDATE `Tabl` SET Score = " + score +
              " WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }
        //*UPDATE <table_name>
        //SET<col_name1> = <value1>, <col_name2> = <value2>, ...
        //WHERE<condition>;
    }
}
