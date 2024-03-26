using ChatProtocol.Reader;
using ChatProtocol.Writer;
using ChatProtocol;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using ChatProtocol.Packets;

namespace ChatServer
{
    public class ServerHandler : ChannelHandlerAdapter
    {
        private Thread _handlerThread;
        private Server _server;
        private IChannelHandlerContext _context;
        private List<RawPacket> _packets;
        private IWriter _writer = new PacketWriter();
        private IReader _reader = new PacketReader();
        private PacketManager _packetManager = new PacketManager();
        private Guid _id;

        public IChannelHandlerContext Context
        {
            get { return _context; }
        }

        public ServerHandler(Server server)
        {
            _handlerThread = new Thread(Handle);
            _server = server;
            _packets = new List<RawPacket>();
            _writer = new PacketWriter();
            _reader = new PacketReader();
            _packetManager.RegisterPacket<Handshake>(0x00);
            _packetManager.RegisterHandler<Handshake>(0x00, HandleHandshake);
            _packetManager.RegisterPacket<ClientMessagePacket>(0x01);
            _packetManager.RegisterHandler<ClientMessagePacket>(0x01, HandleClientMessagePacket);
        }

        public void HandleHandshake(Handshake handshake, EventArgs eventArgs)
        {
            var clients = _server.Clients[_id];
            clients.Nickname = handshake.Nickname;
            Console.WriteLine($"*[{handshake.Nickname}]* dołączył do serwera!");
            
            var clientJoinChatPacket = new ClientJoinChatPacked(_server.Clients[_id].Nickname, _id);
            _server.SendPacketsToAllClients(clientJoinChatPacket);
        }
        
        public void HandleClientMessagePacket(ClientMessagePacket clientMessagePacket, EventArgs eventArgs)
        {
            ServerMessagePacket serverMessagePacket = new ServerMessagePacket();
            serverMessagePacket.Message = clientMessagePacket.Message;
            serverMessagePacket.Id = clientMessagePacket.Id;
            serverMessagePacket.Nickname = clientMessagePacket.Nickname;
            
            Console.WriteLine($"{clientMessagePacket.Nickname}: {clientMessagePacket.Message}");
        
            _server.SendPacketsToAllClients(serverMessagePacket);
        }
        
        public void SendMessageFromClient(string message, string nickname)
        {
            var clientMessagePacket = new ClientMessagePacket
            {
                Message = message,
                Nickname = nickname,
                Id = _id
            };

            _server.SendPacketsToAllClients(clientMessagePacket, _id);
        }

        public void Handle()
        {
            var byteAlocator = new PooledByteBufferAllocator();
            var byteBuffor = byteAlocator.Buffer(8192);
            Console.WriteLine("Uruchomione wątek wysyłania wiadomości");
            lock (byteBuffor)
            {
                _context.WriteAndFlushAsync(Unpooled.WrappedBuffer(byteBuffor));
            }
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _context = context;
            Client client = new Client(this);
            _id = client.Id;
            _server.Clients.Add(client.Id, client);
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            Console.WriteLine("Kanał został pomyślnie zarejestrowany!");
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            var clientQuitChatPacket = new ClientQuitChatPacket(_server.Clients[_id].Nickname, _id); 
            _server.SendPacketsToAllClients(clientQuitChatPacket);
            
            _handlerThread.Interrupt();
            _server.Clients.Remove(_id);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine($"Podczas działania programu wystąpił bład: {exception.Message}");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var dataBuffer = (IByteBuffer)message;
            while (dataBuffer.ReadableBytes > 0)
            {
                var rawPacket = _reader.ReadPacket(dataBuffer); //? - pakiet moze byc nullem
                if (rawPacket == null)
                {
                    continue;
                }
                _packets.Add(rawPacket);
                Console.WriteLine("Dodano pakiet!");
                
                _packetManager.CallAndForgetHandler(rawPacket, new EventArgs());
            }
        }
    }
}
