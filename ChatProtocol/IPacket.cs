using DotNetty.Buffers;

namespace ChatProtocol
{
    public interface IPacket : IDisposable
    {
        byte PackedId();

        PacketDirection Direction();

        void Parse(IByteBuffer byteBuffer); // Parse - konwersja 

        void Serialize(IByteBuffer byteBuffer); // Serialize - przekształcanie obiektów
    }
}