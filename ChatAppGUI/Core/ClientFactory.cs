using System.IO;
using System.Security.Cryptography.X509Certificates;
using ChatAppGUI.Core;
using ChatClient;

public class ClientFactory
{
    private static Client Client;

    static ClientFactory()
    {
        Client = new Client("Karoleq",new X509Certificate2(Path.Combine("", "dotnetty.com.pfx"), "password") , "127.0.0.1", 9000);

        Client.NetworkTask = new Task(() =>
        {
            Client.ConnectToServerAsync().Wait();
        });
        
    Client.NetworkTask.Start();
    }

    public static Client getClientInstance()
    {
        return Client;
    }
}