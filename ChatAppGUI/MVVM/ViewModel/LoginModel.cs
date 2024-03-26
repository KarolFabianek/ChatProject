using ChatAppGUI.Core;
using ChatClient;

namespace ChatAppGUI.MVVM.ViewModel;

public class LoginModel : ObservableObject
{
    private Client _client = ClientFactory.getClientInstance();
}