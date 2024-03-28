using System.Data;
using MySql.Data.MySqlClient;

namespace ChatServer;

public class UserCache
{
    public static readonly string DATABASE_CONNECTION_STRING =
        @"server=mail.paulek.pro;port=3306;userid=karol;password=123456;database=karol";

    public async Task ConnectToDatabaseAsync(string email, string password, string nickname, Guid id, int age)
    {
        using (var connection = new MySqlConnection(DATABASE_CONNECTION_STRING))
        {
            try
            {
                await connection.OpenAsync();

                string insertSql =
                    "INSERT INTO Clients (Email, Password, Nickname, Guid_ID, Age) VALUES (@Email, @Password, @Nickname, @Guid_ID, @Age)";
                var command = new MySqlCommand(insertSql, connection);

                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Nickname", nickname);
                command.Parameters.AddWithValue("@Guid_ID", id);
                command.Parameters.AddWithValue("@Age", age);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd połączenia z bazą danych! {0}", ex);
                throw;
            }
        }
    }

    
    public async Task<bool> CheckCredentialsAsync(string nickname, string password)
    {
        using (var connection = new MySqlConnection(DATABASE_CONNECTION_STRING))
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM Clients WHERE Nickname = @Nickname AND Password = @Password";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nickname", nickname);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(await command.ExecuteScalarAsync());

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd połączenia z bazą danych! {0}", ex);
                throw;
            }
        }
    }
    
    
    public async Task<string> OnlyNickname(string nickname)
    {
        using (var connection = new MySqlConnection(DATABASE_CONNECTION_STRING))
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT Nickname FROM Clients WHERE Nickname = @Nickname AND Password = @Password";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nickname", nickname);

                return nickname;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd połączenia z bazą danych! {0}", ex);
                throw;
            }
        }
    }
}