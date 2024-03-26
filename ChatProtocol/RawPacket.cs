using DotNetty.Buffers;

namespace ChatProtocol
{
    public class RawPacket : IPacket
    {
        private byte _id;
        private int _dataLength;
        private IByteBuffer _payload;

        public RawPacket(){ } // konstruktor bezargumentowym RawPacketu (domyslny)

        public RawPacket(byte id, int dataLength, IByteBuffer payload) // 
        {
            _id = id;
            _dataLength = dataLength; 
            _payload = payload;
        }

        public IByteBuffer Payload
        {
            get { return _payload; }
            set { _payload = value; }
        }

        public int DataLength
        {
            get { return _dataLength; }
            set { _dataLength = value; }
        }

        public byte Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public byte PackedId()
        {
            return _id;     
        }
        
        public PacketDirection Direction()
        {
            throw new Exception("Raw packet is a container, the direction is not known");
        }

        public void Parse(IByteBuffer byteBuffer)
        {
            throw new Exception("Raw packet is a container, cannot parse packet from it");
        }

        public void Serialize(IByteBuffer byteBuffer)
        {
            throw new Exception("Raw packet is a container, cannot serialize from it");
        }

        public void Dispose() {}
    }
}