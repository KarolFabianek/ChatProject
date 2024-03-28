using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ChatAppGUI.MVVM.Model;
using ChatAppGUI.MVVM.ViewModel;
using ChatClient;
using ChatProtocol.Packets;

namespace ChatAppGUI.MVVM.View;

public partial class ChatView : UserControl
{
    private Client _client = ClientFactory.getClientInstance();
    private ChatViewModel _chatViewModel;
public ChatView()
    {
        InitializeComponent();
        _chatViewModel = new ChatViewModel();
        _client.ClientHandler.PacketManager.RegisterHandler<ServerMessagePacket>(0x02, HandleServerMessagePacket);
        DataContext = _chatViewModel;
    }

    private void LeaveChatClick(object sender, RoutedEventArgs e)
    {
        var mainWindow = Application.Current.MainWindow;

        if (mainWindow.DataContext is MainWindowModel mainViewModel)
        {
            mainViewModel.CurrentViewModel = new ChatHistory();
        }
    }

    private void ChatView_Loaded(object sender, RoutedEventArgs e)
    {
        InitializeChat(sender, e);
    }
    
    public void HandleServerMessagePacket(ServerMessagePacket serverMessagePacket, EventArgs eventArgs)
    {
        App.Current.Dispatcher.Invoke((Action)delegate // Trzeba wejsc w thread dispatchera żeby dodać cos do view
        {
            _chatViewModel.Message.Add(new ContextModel(serverMessagePacket.Nickname, DateTime.Now, serverMessagePacket.Message));
        });
    }

    private void InitializeChat(object sender, RoutedEventArgs e)
    {
            ChatViewModel _chatViewModel = new ChatViewModel();
            this.DataContext = _chatViewModel;
    
            Client _client = ClientFactory.getClientInstance();
            var nickname = _client.Nickname;
    
            string message = $"[{nickname}] : {_chatViewModel.Message}";
            Console.WriteLine(message);
     }
    
    private void ReceiveMessageFromClient(string Nickname, DateTime time, string message)
    {
        var viewModel = DataContext as ChatViewModel;
        viewModel?.AddReceivedMessage(Nickname, time, message);
    }

    
    private void SendMessageButton(object sender, RoutedEventArgs e)
    {
        var model = DataContext as ChatViewModel;
        if (model == null)
        {
            return;
        }
        
        var nickname = _client.Nickname;
        string message = MessageTextBox.Text;

        string fullMessage = $"[{nickname}]: {message}";

        // model.Message.Add(new ContextModel(nickname, DateTime.Now, message));
        _client.ClientHandler.SendMessage(message);

        MessageTextBox.Text = "";

        if (message.Contains("!Clear Chat"))
        {
            model.ClearChat(); 
        }
    }
}
