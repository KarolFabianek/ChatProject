using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ChatProtocol.Packets;
using ChatServer;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace ChatServerWithDatabase
{
    class Program
    {

        public ClientRegisterPacket RegisterData;

        public static readonly string DATABASE_CONNECTION_STRING =
            @"server=mail.paulek.pro;port=3306;userid=karol;password=123456;database=karol";

        public static async Task Main(string[] args)
        {
            Server server = new Server(new X509Certificate2(Path.Combine("", "dotnetty.com.pfx"), "password"));
            Console.WriteLine("Serwer startuje...");
            await server.RunServerAsync();
            //await ConnectToDatabaseAsync();

        }
    }
}






