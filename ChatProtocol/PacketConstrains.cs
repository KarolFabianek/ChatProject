namespace ChatProtocol
{
    public class PacketConstrains
    {
        public const int ProtocolNumber = 1;
        public const int Threshold = 8192; // 8192 - maksymalna liczba bitów w naszym programiw
        public const int MaxPacketId = 10;
        public const int MaxPacketLength = 10000; 
    }
}