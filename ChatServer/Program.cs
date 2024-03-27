using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ChatServer;
using Microsoft.Extensions.Logging;
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
            using (var connection = new MySqlConnection(DATABASE_CONNECTION_STRING)) 
            {
                // try
                // {
                //     connection.Open();
                //     string exampleSql = "SELECT * FROM clients";
                //     string insertSql =
            //         "INSERT INTO Clients (ID,Email,Password,Nickname,Guid_ID, Age) VALUES ('6969696969','MarianPazdzioch@wp.pl','Agata123@$',Fagaciara','69123469','13')";
                //     var command = new MySqlCommand(exampleSql, connection);
                //     var reader = command.ExecuteReader();
                //     while (reader.Read())
                //     {
                //         // Client _newClient = new Client(); 
                //         // Console.WriteLine($"{_newClient.Content}");                        
                //     }
                // }
                // catch (Exception ex) 
                // {
                //    Console.WriteLine("Błąd połaczenia z bazą danych!"); 
                //    return;
                // }
                // finally 
                // {
                //     if (connection.State != ConnectionState.Closed)
                //     {
                //         connection.Close();
                //     }
                //     connection.Dispose(); 
                // }
            }
        }
        
        
        
        
        
        // using (var connection = new MySqlConnection(DATABASE_CONNECTION_STRING)) //do () mozesz odwolac sie tylko w klamrach usinga
        // {
        //     try
        //     {
        //         connection.Open();
        //         Logger.Log(LogLevel.Info, $"Saving message {message.Id.ToString()} to database");
        //         string insertSql = $"INSERT INTO karol.messages (discord_id,author_id,author_name,content,guild_id) VALUES ('{e.Message.Id.ToString()}', '{e.Message.Author.Id.ToString()}', '{e.Author.Username}','{e.Message.Content}','{e.Guild.Id.ToString()}')";
        //         var command = new MySqlCommand(insertSql, connection);
        //         int rowsEffectedcommand = command.ExecuteNonQuery();     
        //         if (rowsEffectedcommand < 1)
        //         {
        //             Logger.Log(LogLevel.Error, $"Error while saving message {message.Id.ToString()} to database");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Logger.Log(LogLevel.Error, $"Error while Maryla Learning: {ex.Message}", ex);
        //         return;
        //     }
        //     finally
        //     {
        //         if (connection.State != ConnectionState.Closed)
        //         {
        //             connection.Close();
        //         }
        //         connection.Dispose(); //upewnia sie czy cala pamiec zostanie wyzerowana
        //     }
        // }
        
        
        
        
        
    }
}


//git add .
//git commit -a -m "Some changes"
//git push -u 

