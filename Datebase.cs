using MySqlConnector;

namespace Lab2
{
    public class Datebase
    {
        public MySqlConnection connection;
        public MySqlCommand cmd = new MySqlCommand();
        public Datebase()
        {

            using MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=db-102");
            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS `Tabl` (" +
                "id SERIAL PRIMARY KEY, " +
                "Color VARCHAR(15), " +
                "Score int DEFAULT 0)";
            //cmd = "CREATE TABLE IF EXISTS Score";
            cmd.Connection = connection;
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
            cmd.CommandText = "SELECT" + "Score" +
                "FROM `Tabl` WHERE id=" + id;
            cmd.ExecuteNonQuery();
            MySqlDataReader reader = cmd.ExecuteReader();
            int result = 0;
            while (reader.Read())
            {
                result = reader.GetInt32(0);
            }
            return result;
        }
    }
}
