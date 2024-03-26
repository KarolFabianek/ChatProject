using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ChatServer;
using MySql.Data.MySqlClient;

namespace ChatServerWithDatabase
{
    class Program
    {
        public static readonly string DATABASE_CONNECTION_STRING = @"server=mail.paulek.pro;port=3306;userid=karol;password=123456;database=karol-tekstowy";
        
        public static async Task Main(string[] args)
        {
            Server server = new Server(new X509Certificate2(Path.Combine("", "dotnetty.com.pfx"), "password"));
            Console.WriteLine("Serwer startuje...");
            await server.RunServerAsync();
            await ConnectToDatabaseAsync();
            
        }
        
        public static async Task ConnectToDatabaseAsync()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING))
                {
                    await connection.OpenAsync();
                    Console.WriteLine("Połączono z bazą danych!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z bazą danych: {ex.Message}");
            }
        }
    }
}


//git add .
//git commit -a -m "Some changes"
//git -push 