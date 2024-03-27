using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Channels;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using ChatProtocol;
using ChatProtocol.Packets;
using ChatProtocol.Writer;
using DotNetty.Handlers.Tls;
using Org.BouncyCastle.Bcpg;

namespace ChatServer
{
    public class Server
    {
        private X509Certificate2 _serverCertificate;
        private List<string> _receivedMessageHistory;
        public List<SentMessage> SentMessageHistory;
        private Dictionary<Guid, Client> _clients = new Dictionary<Guid, Client>();
        private Guid _id;
        
        public Server(X509Certificate2 serverCertificate) // konstruktor klasy Server
        {
            _serverCertificate = serverCertificate;
            _receivedMessageHistory = new List<string>();
            SentMessageHistory = new List<SentMessage>();
            _id = Guid.NewGuid();
        }

        public class SentMessage // publiczna klasa ,,SentMessage''
        {
            public string Sender { get; set; } //gettery i settery
            public string Message { get; set; }  //gettery i settery
        }

        public Dictionary<Guid, Client> Clients //mozna tak 
        {
            get { return _clients; }
            set { _clients = value; }
        }
        
        public X509Certificate2 ServerCertificate
        {
            get { return _serverCertificate; }
            set { _serverCertificate = value; }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public void SendPacketsToAllClients(IPacket packet, params Guid[] blackListedId)   // [] - do okreslania typow Arraya
        {
            foreach (var client in Clients.Values)
            {
                if (blackListedId.Contains(client.Id))
                {
                    continue;
                }
                var writer = new PacketWriter();
                writer.WritePacket(packet);
                writer.Flush(client.Handler.Context);
            }
        }
        
        public async Task RunServerAsync()
        {
            var mainThreadsGroup = new MultithreadEventLoopGroup(1);
            var workersThreadsGroup = new MultithreadEventLoopGroup(1);
            var boostrap = new ServerBootstrap();

            try
            {
                boostrap
                    .Group(mainThreadsGroup, workersThreadsGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 500) // bedzie wypisywalo wszystkie błędy
                    .Handler(new LoggingHandler(LogLevel.INFO))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        var pipeline = channel.Pipeline;
                        pipeline.AddLast(new ServerHandler(this)); // this - odwołanie do aktywnej instancji klasy
                        pipeline.AddLast(TlsHandler.Server(ServerCertificate));
                    }));

                var bootstrapChannel = await boostrap.BindAsync(IPAddress.Parse("127.0.0.1"), 9000);
                for(;;) {}
                await bootstrapChannel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            finally
            {
                Task.WaitAll(mainThreadsGroup.ShutdownGracefullyAsync(), workersThreadsGroup.ShutdownGracefullyAsync());  // WaitAll - wstrzymuje działanie pozostałych metod
            } 
        }
        
        public List<string> ReceivedMessageHistory
        {
            get { return _receivedMessageHistory; }  
            set { _receivedMessageHistory = value; } 
        }
        
    }
}
