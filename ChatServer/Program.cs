using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ChatServer;
using MySql.Data.MySqlClient;

namespace ChatServerWithDatabase
{
    class Program
    {
        public static readonly string DATABASE_CONNECTION_STRING = @"server=mail.paulek.pro;port=3306;userid=karol;password=123456;database=karol";
        
        public static async Task Main(string[] args)
        {
            Server server = new Server(new X509Certificate2(Path.Combine("", "dotnetty.com.pfx"), "password"));
            Console.WriteLine("Serwer startuje...");
            ConnectToDatabaseAsync();
            await server.RunServerAsync();
            //await ConnectToDatabaseAsync();
            
        }
        
        public static async Task ConnectToDatabaseAsync()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("Udało się połączyć z bazą danych!");
                        connection.Open();
                        string insertQuery = "INSERT INTO users (nickname, password) VALUES (@nickname, @password)";
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@nickname", "user123");
                        command.Parameters.AddWithValue("@password", "hashed_password"); 
                        command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z bazą danych: {ex.Message}");
            }
        }
        
        public static async Task<bool> ValidateCredentialsAsync(string nickname, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING))
                {
                    await connection.OpenAsync();
                    string query = "SELECT COUNT(*) FROM users WHERE nickname = @nickname AND password = @password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nickname", nickname);
                    command.Parameters.AddWithValue("@password", password);
                    
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd weryfikacji danych w bazie danych: {ex.Message}");
                return false;
            }
        }
        
        
    }
}


//git add .
//git commit -a -m "Some changes"
//git push -u 

