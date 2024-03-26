using System.Text;
using DotNetty.Buffers;
using Microsoft.VisualBasic.FileIO;

namespace ChatProtocol.Packets;

public class ClientRegisterPacket : IPacket
{
    private string _login;
    private string _password;
    private Guid _id;
    private string _nickname;
    private int _age;
    private string _email;
    
    
    public void Dispose()
    {
        _login = "";
        _password = "";
        _email = "";
        _age = 0;
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
        var loginLength = byteBuffer.ReadInt();
        _login = byteBuffer.ReadString(loginLength, Encoding.Default);

        var passwordLength = byteBuffer.ReadInt();
        _password = byteBuffer.ReadString(passwordLength, Encoding.Default);

        var guidLength = byteBuffer.ReadInt();
        var guidString = byteBuffer.ReadString(guidLength, Encoding.Default);
        _id = new Guid(guidString);

        var nicknameLength = byteBuffer.ReadInt();
        _nickname = byteBuffer.ReadString(nicknameLength, Encoding.Default);

        var ageLength = byteBuffer.ReadInt();

        var emailLength = byteBuffer.ReadInt();
        _email = byteBuffer.ReadString(emailLength, Encoding.Default);
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
        
        byteBuffer.WriteInt(_age);

        byteBuffer.WriteInt(_email.Length);
        byteBuffer.WriteString(_email, Encoding.Default);
    }
}