using System.Text;
using DotNetty.Buffers;

namespace ChatProtocol.Packets;

public class ClientLoginPacket : IPacket
{
    private string _login;
    private string _password;
    private Guid _id;
    private string _nickname;

    public void Dispose()
    {
        _login = "";
        _password = ""; 
    }
    
    public byte PackedId()
    {
        return 0x05;
    }

    public PacketDirection Direction()
    {
        return PacketDirection.ClientBound;
    }

    public void Parse(IByteBuffer byteBuffer)
    {
        var loginLength = byteBuffer.ReadInt();
        _login = byteBuffer.ReadString(loginLength, Encoding.Default);

        var passwordLength = byteBuffer.ReadInt();
        _password = byteBuffer.ReadString(passwordLength, Encoding.Default);

        var guidLength = byteBuffer.ReadInt();
        var guidString = byteBuffer.ReadString(guidLength, Encoding.Default);
        _id = new Guid(guidString);

        var nicknameLength = byteBuffer.ReadInt();
        _nickname = byteBuffer.ReadString(nicknameLength, Encoding.Default);
    }

    public void Serialize(IByteBuffer byteBuffer)
    {
        byteBuffer.WriteInt(_login.Length);
        byteBuffer.WriteString(_login, Encoding.Default);
        
        byteBuffer.WriteInt(_password.Length);
        byteBuffer.WriteString(_password, Encoding.Default);
            
        var guidHelp = _id.ToString();
        byteBuffer.WriteInt(guidHelp.Length);
        byteBuffer.WriteString(guidHelp, Encoding.Default);
            
        byteBuffer.WriteInt(_nickname.Length);
        byteBuffer.WriteString(_nickname, Encoding.Default);
    }
}