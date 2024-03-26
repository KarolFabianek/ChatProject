using DotNetty.Buffers;
using System.Text;

namespace ChatProtocol.Packets
{
    public class ClientMessagePacket : IPacket
    {
        private string _message;
        private Guid _id;
        private string _nickname;

        public ClientMessagePacket() { }
        
        public ClientMessagePacket(string message, Guid id, string nickname)
        {
            _message = message;
            _id = id;
            _nickname = nickname;
        }
        
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }
        
        public byte PackedId()
        {
            return 0x01;
        }

        public PacketDirection Direction()
        {
            return PacketDirection.ServerBound;
        }

        public void Parse(IByteBuffer byteBuffer)
        {
            var messageLength = byteBuffer.ReadInt();
            _message = byteBuffer.ReadString(messageLength, Encoding.Default);

            var guidLength = byteBuffer.ReadInt();
            var guidString = byteBuffer.ReadString(guidLength, Encoding.Default);
            _id = new Guid(guidString);

            var nicknameLength = byteBuffer.ReadInt();
            _nickname = byteBuffer.ReadString(nicknameLength, Encoding.Default);
        }

        public void Serialize(IByteBuffer byteBuffer)
        {
            byteBuffer.WriteInt(_message.Length);
            byteBuffer.WriteString(_message, Encoding.Default);
            
            var guidHelp = _id.ToString();
            byteBuffer.WriteInt(guidHelp.Length);
            byteBuffer.WriteString(guidHelp, Encoding.Default);
            
            byteBuffer.WriteInt(_nickname.Length);
            byteBuffer.WriteString(_nickname, Encoding.Default);
        }

        public void Dispose() // funkcja sluzaca do "pozbywania sie" zmiennych
        {
            _message = null;
            _nickname = null;
        }
    }
}
