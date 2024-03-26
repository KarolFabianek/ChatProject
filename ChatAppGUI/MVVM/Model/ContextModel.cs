using Org.BouncyCastle.Crypto.Tls;

namespace ChatAppGUI.MVVM.Model;

public class ContextModel
{
    public string Nickname { get; set; }
    public DateTime Time { get; set; }
    public string Message { get; set; }


    public ContextModel(string nickname, DateTime time, string message)
    {
        Nickname = nickname;
        Time = time;
        Message = message;
    }
        
}