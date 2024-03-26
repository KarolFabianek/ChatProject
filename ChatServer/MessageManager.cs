public class MessageManager
{
    private List<Message> receivedMessageHistory = new List<Message>();
    private List<Message> sentMessageHistory = new List<Message>();
    private Message _message;

    public event EventHandler<MessageEventArgs> NewReceivedMessage;

    public event EventHandler<MessageEventArgs> NewSentMessage;

    public MessageManager(Message message)
    {
        message = _message;
    }

    public MessageManager() { }

    public async Task SendMessage(string sender, string message)
    {
        await Task.Delay(100);
        var newMessage = new Message(sender, message);
        sentMessageHistory.Add(newMessage);
        NewSentMessage?.Invoke(this, new MessageEventArgs(newMessage));
    }

    public async Task<IEnumerable<Message>> GetReceivedMessageHistoryFromServer()
    {
        await Task.Delay(100);
        return receivedMessageHistory;
    }

    public async Task<IEnumerable<Message>> GetSentMessageHistoryFromServer()
    {
        await Task.Delay(100);
        return sentMessageHistory;
    }

    public async Task ReceiveMessage(Message message)
    {
        await Task.Delay(100);
        receivedMessageHistory.Add(message);
        NewReceivedMessage?.Invoke(this, new MessageEventArgs(message));
    }
}

public class MessageEventArgs : EventArgs
{
    public Message Message { get; }

    public MessageEventArgs(Message message)
    {
        Message = message;
    }
}

public class Message
{
    private string _sender;
    private string _content;

    public string Sender
    {
        get { return _sender; }
        set { _sender = value; }
    }

    public string Content
    {
        get { return _content; }
        set { _content = value; }
    }

    public Message(string sender, string content)
    {
        Sender = sender;
        Content = content;
    }
    
}