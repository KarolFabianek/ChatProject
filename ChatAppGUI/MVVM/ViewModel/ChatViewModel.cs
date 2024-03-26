using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ChatAppGUI.MVVM.Model;
using ChatAppGUI.MVVM.ViewModel;
using ChatClient;
using ChatProtocol;
using ChatProtocol.Packets;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Bcpg;
using Client = ChatClient.Client;


namespace ChatAppGUI.MVVM.ViewModel
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private Client _client = ClientFactory.getClientInstance();
        public string Nickname { get; set; }
        public ObservableCollection<ContextModel> Message { get; set; }
        public ObservableCollection<ContactModel> Information { get; set; }
        
        private string _newMessage;

        public ChatViewModel()
        {
            Nickname = _client.Nickname;
              
            Message = new ObservableCollection<ContextModel>();
            Information = new ObservableCollection<ContactModel>();
            
        
            
            Information.Add(new ContactModel
            {
                Nickname = "Magik bedzie",
                Id = 11212,
                Information = "Ech"
            });
        }
        

        
        public void AddReceivedMessage(string Nickname, DateTime time, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Message.Add(new ContextModel(Nickname, time, message));
            });
        }
        
        public void ReceiveMessage(Client client)
        {
            if (client == null)
                return;

            string message = NewMessage;
            string fullMessage = $"[{Nickname}]: {message}";
            
            var clientMessagePacket = new ClientMessagePacket
            {
                Message = message,
                Nickname = client.Nickname,
            };

            // if (message.Length == 0)
            // {
            //     RemoveChatMessage();
            // }
            
            client.ClientHandler.SendMessage(message);
            
            Message.Add(new ContextModel(Nickname, DateTime.Now, message));
            
            NewMessage = "";
            
            if (message.Contains("!Clear Chat"))
            {
                ClearChat();
            }
        }
        
        public void ClearChat()
        {
            Message.Clear(); // Usunięcie wszystkich wiadomości z listy
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                if (_newMessage != value)
                {
                    _newMessage = value;
                    OnPropertyChanged(nameof(NewMessage));
                }
            }
        }
    }
}