using System.Text;
using DotNetty.Buffers;

namespace ChatProtocol.Packets;

public class ClientLoginRespond : IPacket
{
    private Guid _id;
    private string _secret;
    private string _nickname;

    public void Dispose()
    {
        _nickname = "";
        _secret = ""; 
    }
    
    public byte PackedId()
    {
        return 0x06;
    }

    public PacketDirection Direction()
    {
        return PacketDirection.ClientBound;
    }

    public void Parse(IByteBuffer byteBuffer)
    {
        var nicknameLength = byteBuffer.ReadInt();
        _nickname = byteBuffer.ReadString(nicknameLength, Encoding.Default);

        var secretLength = byteBuffer.ReadInt();
        _secret = byteBuffer.ReadString(secretLength, Encoding.Default);
        
        var guidHelp = _id.ToString();
        byteBuffer.WriteInt(guidHelp.Length);
        byteBuffer.WriteString(guidHelp, Encoding.Default);
    }

    public void Serialize(IByteBuffer byteBuffer)
    {
        byteBuffer.WriteInt(_nickname.Length);
        byteBuffer.WriteString(_nickname, Encoding.Default);
        
        byteBuffer.WriteInt(_secret.Length);
        byteBuffer.WriteString(_secret, Encoding.Default);
        
        var guidHelp = _id.ToString();
        byteBuffer.WriteInt(guidHelp.Length);
        byteBuffer.WriteString(guidHelp, Encoding.Default);
    }
}