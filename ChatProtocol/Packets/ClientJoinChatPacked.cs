using System.Text;
using DotNetty.Buffers;

namespace ChatProtocol.Packets;

public class ClientJoinChatPacked : IPacket
{
    private string _nickname;
    private Guid _id;
    
    public ClientJoinChatPacked() { }
    
    public ClientJoinChatPacked(string nickname, Guid id)
    {
        _nickname = nickname;
        _id = id;
    }
    
    public string Nickname
    {
        get { return _nickname; }
        set { _nickname = value; }
    }

    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public void Parse(IByteBuffer byteBuffer)
    {
        var guidLength = byteBuffer.ReadInt();
        var guidString = byteBuffer.ReadString(guidLength, Encoding.Default);
        _id = new Guid(guidString);
        
        var nicknameLength = byteBuffer.ReadInt();
        _nickname = byteBuffer.ReadString(nicknameLength, Encoding.Default);
    }
        
    public void Serialize(IByteBuffer byteBuffer)
    {
        var guidHelp = _id.ToString();
        byteBuffer.WriteInt(guidHelp.Length);
        byteBuffer.WriteString(guidHelp, Encoding.Default);
        
        byteBuffer.WriteInt(_nickname.Length);
        byteBuffer.WriteString(_nickname, Encoding.Default);
    }
        
    public byte PackedId()
    {
        return 0x04;
    }

    public PacketDirection Direction()
    {
        return PacketDirection.ClientBound;
    }

    public void Dispose()
    {

    }
}