namespace ChatServer;

public class Client 
{
    private Guid _id;
    private string _nickname;
    private ServerHandler _handler;
    
    public Task NetworkTask { get; set; }

    public Client(Guid id, string nickname, ServerHandler handler)
    {
        _id = id;
        _nickname = nickname;
        _handler = handler;
    }

    public Client(Guid id, ServerHandler handler)
    {
        _id = id;
        _handler = handler;
    }

    public Client(ServerHandler handler)
    {
        _handler = handler;
        _id = Guid.NewGuid();
    }

    public Client()
    {
 
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

    public ServerHandler Handler
    {
        get { return _handler; }
        set { _handler = value; }
    }
}