using System.Data.SQLite;
using System.Diagnostics;


namespace TP2.Models
{
public class Personal_info
    {
        private static string dbSource = "Data Source=C:\\Users\\Yassine\\source\\repos\\TP2\\db\\database.db; Version = 3; New = True; Compress = True;";
        public Person[] GetAllPerson()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection(dbSource);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = " SELECT Count(*) FROM personal_info";

            //SQLiteDataReader resultSize = sqlite_cmd.ExecuteReader();
            Person[] Result = new Person[(Int64)sqlite_cmd.ExecuteScalar()];
       

            SQLiteCommand sqlite_cmd_get_users;
            sqlite_cmd_get_users = sqlite_conn.CreateCommand();
            sqlite_cmd_get_users.CommandText = "SELECT * FROM personal_info";
            SQLiteDataReader resultUsers = sqlite_cmd_get_users.ExecuteReader();






            int i = 0;
            using (resultUsers)
            {
                while (resultUsers.Read())
                {
                    int id = (int)resultUsers["id"];
                    string first_name = (string)resultUsers["first_name"];
                    string last_name = (string)resultUsers["last_name"];
                    string email = (string)resultUsers["email"];
                    //DateTime date_birth = DateTime.ParseExact((String)results["date_birth"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string image = (string)resultUsers["image"];
                    string country = (string)resultUsers["country"];
                    Result[i] = new Person(id, first_name, last_name, email, image, country) ;
                    i++;
                }


            }
            sqlite_conn.Close();
            return Result;
        }

        public Person GetPerson(int idQuery)
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection(dbSource);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            SQLiteCommand sqlite_cmd_get_id;
            sqlite_cmd_get_id = sqlite_conn.CreateCommand();
            sqlite_cmd_get_id.CommandText = "SELECT * FROM personal_info WHERE id = TRIM(@id)";
            sqlite_cmd_get_id.Parameters.AddWithValue("id", idQuery);

            sqlite_cmd_get_id.ExecuteNonQuery();
            SQLiteDataReader resultUsers = sqlite_cmd_get_id.ExecuteReader();

            Person ResultPerson = null;

            using (resultUsers)
            {
                while (resultUsers.Read())
                {
                    int id = (int)resultUsers["id"];
                    string first_name = (string)resultUsers["first_name"];
                    string last_name = (string)resultUsers["last_name"];
                    string email = (string)resultUsers["email"];
                    //DateTime date_birth = DateTime.ParseExact((String)results["date_birth"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string image = (string)resultUsers["image"];
                    string country = (string)resultUsers["country"];
                    ResultPerson = new Person(id, first_name, last_name, email, image, country);
                }
            }

            sqlite_conn.Close();

            return ResultPerson;
        }

        public Person GetPersonBySearch(string firstName, string countrySearch)
        {
            
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection(dbSource);

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception err)
            {

            }
            SQLiteCommand sqlite_query;
            sqlite_query = sqlite_conn.CreateCommand();
            sqlite_query.CommandText = "SELECT * FROM personal_info WHERE TRIM(UPPER(first_name)) = TRIM(UPPER(@firstName)) AND TRIM(UPPER(country)) = TRIM(UPPER(@country))";
            sqlite_query.Parameters.AddWithValue("firstName", firstName);
            sqlite_query.Parameters.AddWithValue("country", countrySearch);

            sqlite_query.ExecuteNonQuery();
            SQLiteDataReader resultUser = sqlite_query.ExecuteReader();

            Person resultPerson = null;
            using (resultUser)
            {
                while (resultUser.Read())
                {
                    int id = (int)resultUser["id"];
                    string first_name = (string)resultUser["first_name"];
                    string last_name = (string)resultUser["last_name"];
                    string email = (string)resultUser["email"];
                    //DateTime date_birth = DateTime.ParseExact((String)results["date_birth"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string image = (string)resultUser["image"];
                    string country = (string)resultUser["country"];
                    resultPerson = new Person(id, first_name, last_name, email, image, country);
                }
            }

            sqlite_conn.Close();

            return resultPerson;
        }
    }
}
