using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProtocol.Packets
{
    public class Handshake : IPacket
    {
        private string _nickname;
        
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        public void Parse(IByteBuffer byteBuffer)
        {
            var nicknameLength = byteBuffer.ReadInt();
            _nickname = byteBuffer.ReadString(nicknameLength, Encoding.Default);
        }
        
        public void Serialize(IByteBuffer byteBuffer)
        {
            byteBuffer.WriteInt(_nickname.Length);
            byteBuffer.WriteString(_nickname, Encoding.Default);
        }
        
        public byte PackedId()
        {
            return 0x00;
        }

        public PacketDirection Direction()
        {
            return PacketDirection.ServerBound;
        }

        public void Dispose()
        {

        }
    }
}