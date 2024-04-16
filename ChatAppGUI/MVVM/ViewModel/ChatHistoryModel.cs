using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ChatAppGUI.MVVM.Model;
using ChatAppGUI.MVVM.ViewModel;
using ChatClient;
using ChatProtocol;
using ChatProtocol.Packets;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Bcpg;
using Client = ChatClient.Client;


namespace ChatAppGUI.MVVM.ViewModel
    {
        public class ChatHistoryModel : INotifyPropertyChanged
        {
            private string _userName;
            private string _message;
            private string _sender;


            public ChatHistoryModel()
            {
                _userName = "[Fabianito]";
                _message = "Kocham Placki";
                _sender = "[Pan Jedrzej]";
            }

            public string UserName
            {
                get => _userName;
                set
                {
                    if (_userName != value)
                    {
                        _userName = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Message
            {
                get => _message;
                set
                {
                    if (_message != value)
                    {
                        _message = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Sender
            {
                get => _sender;
                set
                {
                    if (_sender != value)
                    {
                        _sender = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        
}
















