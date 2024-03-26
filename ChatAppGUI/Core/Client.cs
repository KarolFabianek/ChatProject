using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace ChatClient
{
    public class Client
    {
        private string _nickname;
        private X509Certificate2 _certificate;
        private string _ipAddress; 
        private int _port;
        private ClientHandler _clientHandler;
        
        public Task NetworkTask { get; set; }

        public ClientHandler ClientHandler
        {
            get { return _clientHandler; }
        }
        
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        public X509Certificate2 Certificate
        {
            get { return _certificate; }
            set { _certificate = value; }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        
        public Client(string nickname, X509Certificate2 certificate, string ipAddress, int port)
        {
            _nickname = nickname;
            _certificate = certificate;
            _ipAddress = ipAddress;
            _port = port;
            _clientHandler =  new ClientHandler(this);
        }
        
        public async Task ConnectToServerAsync()
        {
            var mainThreadGroup = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();

            try
            {
                bootstrap
                    .Group(mainThreadGroup)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        var pipeline = channel.Pipeline;
                        pipeline.AddLast(_clientHandler);
                        pipeline.AddLast(
                            new TlsHandler(
                                stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true),
                                new ClientTlsSettings(_certificate.GetNameInfo(X509NameType.DnsName, false))));
                    }));

                var bootstrapChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(_ipAddress), _port));
                for (;;) { }

                await bootstrapChannel.CloseAsync();
            }
            catch (Exception ex) { }

            finally
            {
                Task.WaitAll(mainThreadGroup.ShutdownGracefullyAsync());
            }
        }
    }
}
