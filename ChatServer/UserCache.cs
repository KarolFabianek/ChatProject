using System.Data;
using MySql.Data.MySqlClient;

namespace ChatServer;

public class UserCache 
{
    public static readonly string DATABASE_CONNECTION_STRING = @"server=mail.paulek.pro;port=3306;userid=karol;password=123456;database=karol";

    public async Task ConnectToDatabaseAsync(string email,string password,string nickname,Guid id,int age)
    {
        using (var connection = new MySqlConnection(DATABASE_CONNECTION_STRING)) 
        {
            try
            {
                connection.Open();
                string insertSql = "INSERT INTO Clients (Email,Password,Nickname,Guid_ID, Age) VALUES ('MarianPazdzioch@wp.pl','Agata123@$','Fagaciara','69123469','13')";
                var command = new MySqlCommand(insertSql, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Błąd połaczenia z bazą danych! {0}", ex); 
                return;
            }
            finally 
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                connection.Dispose(); 
            }
        }
    }
}