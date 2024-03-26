using DotNetty.Transport.Channels;
using DotNetty.Buffers;
using ChatProtocol;
using ChatProtocol.Packets;
using ChatProtocol.Reader;
using ChatProtocol.Writer;

namespace ChatClient
{
    public class ClientHandler : ChannelHandlerAdapter
    {
        private Thread _handlerThread;
        private Client _client;
        private IChannelHandlerContext _context;
        private List<RawPacket> _packets;
        private IWriter _writer = new PacketWriter();
        private IReader _reader = new PacketReader();
        private PacketManager _packetManager = new PacketManager();

        public PacketManager PacketManager
        {
            get { return _packetManager; }
            set { _packetManager = value; }
        }
        
        public ClientHandler(Client client)
        {
            _client = client;
            _handlerThread = new Thread(Handle);
            _packets = new List<RawPacket>();
            _writer = new PacketWriter();
            _reader = new PacketReader();       
            _packetManager.RegisterPacket<ServerMessagePacket>(0x02);
            
            _packetManager.RegisterPacket<ClientJoinChatPacked>(0x04);
            _packetManager.RegisterHandler<ClientJoinChatPacked>(0x04, HandleClientJoinChatPacket);
            
            _packetManager.RegisterPacket<ClientQuitChatPacket>(0x03);
            _packetManager.RegisterHandler<ClientQuitChatPacket>(0x03, HandleClientQuitChatPacket);
        }
        

        public void HandleClientJoinChatPacket(ClientJoinChatPacked serverMessagePacket, EventArgs eventArgs)
        {
            Console.WriteLine($"[{serverMessagePacket.Nickname}] dołączył do czatu!");
        }

        public void HandleClientQuitChatPacket(ClientQuitChatPacket serverMessagePacket, EventArgs eventArgs)
        {
            Console.WriteLine($"[{serverMessagePacket.Nickname}] opuscil czat!");
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
                _packetManager.CallAndForgetHandler(rawPacket, new EventArgs());
            }
        }

        public void Handle()
        {
            var handshake = new Handshake();
            handshake.Nickname = _client.Nickname;
            _writer.WritePacket(handshake);
            _writer.Flush(_context);

            // while (true)
            // {
            //     var message = Console.ReadLine();
            //     var messagePacket = new ClientMessagePacket(message, Guid.NewGuid(), _client.Nickname);
            //     _writer.WritePacket(messagePacket);
            //     _writer.Flush(_context); //Flush - wysyla ByteStream
            // }
        }

        public void SendMessage(string message)
        {
                var messagePacket = new ClientMessagePacket(message, Guid.NewGuid(), _client.Nickname);
                _writer.WritePacket(messagePacket);
                _writer.Flush(_context);
        }
        
        public override void ChannelActive(IChannelHandlerContext context)
        {
            _context = context;
            Console.WriteLine("Połączono z serwerem!");
            _handlerThread.Start();
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            _handlerThread.Interrupt();
        }
    }
}
