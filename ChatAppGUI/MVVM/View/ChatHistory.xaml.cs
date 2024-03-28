using System.Windows;
using ChatAppGUI.MVVM.ViewModel;
using DotNetty.Transport.Channels;

namespace ChatAppGUI.MVVM.View;

public partial class ChatHistory 
{
    public ChatHistory()
    {
        InitializeComponent();
    }
    
    private void RateButton_Click(object sender, RoutedEventArgs e)
    {
        int stars = (int)starSlider.Value;
        if (stars == 0)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdek - zostań jeszcze chwile przekonamy Cie ze jest spoko");
        }
        
        if (stars == 1)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdek - Jeden to nasz numer w rankingu Chatów w Polsce. Napewno zle kliknales!");
        }
        
        if (stars == 2)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdek - 2 to bedziesz mial z egzaminu jak nas tak ocenisz");
        }
        
        if (stars == 3)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdek - Jest 3 jest stabilnie");
        }
        
        if (stars == 4)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdek - Za słabo przeciagnales, jeszcze troche w prawo");
        }
        
        if (stars == 5)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdek - Jestes Prezesem, witamy w naszym elitarnym gronie");
        }
    }
}










// using System.Collections.ObjectModel;
// using System.IO;
// using System.Windows;
// using System.Windows.Controls;
// using ChatAppGUI.MVVM.Model;
// using ChatAppGUI.MVVM.ViewModel;
// using ChatClient;
// using ChatProtocol.Packets;
//
// namespace ChatAppGUI.MVVM.View;
//
// public partial class ChatHistory : UserControl
// {
//     private Client _client = ClientFactory.getClientInstance();
//     private ChatHistoryModel _chatHistoryModel;
// public ChatHistory()
//     {
//         InitializeComponent();
//         _chatHistoryModel = new ChatHistoryModel();
//         _client.ClientHandler.PacketManager.RegisterHandler<ServerMessagePacket>(0x02, HandleServerMessagePacket);
//         DataContext = _chatHistoryModel;
//     }
//
//     private void LeaveButton(object sender, RoutedEventArgs e)
//     {
//         var mainWindow = Application.Current.MainWindow;
//
//         if (mainWindow.DataContext is MainWindowModel mainViewModel)
//         {
//             mainViewModel.CurrentViewModel = new ChatHistory();
//         }
//     }
//
//     private void ChatHistory_Loaded(object sender, RoutedEventArgs e)
//     {
//         InitializeChat(sender, e);
//     }
//     
//     public void HandleServerMessagePacket(ServerMessagePacket serverMessagePacket, EventArgs eventArgs)
//     {
//         App.Current.Dispatcher.Invoke((Action)delegate // Trzeba wejsc w thread dispatchera żeby dodać cos do view
//         {
//             _chatHistoryModel.Message.Add(new ContextModel(serverMessagePacket.Nickname, DateTime.Now, serverMessagePacket.Message));
//         });
//     }
//
//     private void InitializeChat(object sender, RoutedEventArgs e)
//     {
//             ChatViewModel _chatHistoryModel = new ChatViewModel();
//             this.DataContext = _chatHistoryModel;
//     
//             Client _client = ClientFactory.getClientInstance();
//             var nickname = _client.Nickname;
//     
//             string message = $"[{nickname}] : {_chatHistoryModel.Message}";
//             Console.WriteLine(message);
//      }
//     
//     private void ReceiveMessageFromClient(string Nickname, DateTime time, string message)
//     {
//         var viewModel = DataContext as ChatViewModel;
//         viewModel?.AddReceivedMessage(Nickname, time, message);
//     }
// }
//


