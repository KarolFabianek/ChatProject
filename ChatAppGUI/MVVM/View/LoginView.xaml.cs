using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ChatAppGUI.MVVM.ViewModel;
using ChatServer;
using Client = ChatClient.Client;

namespace ChatAppGUI.MVVM.View;

public partial class LoginView : UserControl
{
    private Client _client = ClientFactory.getClientInstance();
    private UserCache userCache = new UserCache();
    public LoginView()
    {
        InitializeComponent();
    }

    private async void CheckDataClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string password = txtPassword.Password;
            string nickname = txtNickname.Text;
        
            string encryptedPasswordLogin = EncryptPasswordLogin(password);

            bool isAuthenticated = await userCache.CheckCredentialsAsync(nickname, encryptedPasswordLogin);

            if (isAuthenticated)
            {
                MessageBox.Show("Logowanie udane!");
            
                string nicknameLogin = await userCache.OnlyNickname(nickname);
                _client.Nickname = nicknameLogin;
            
                var mainWindow = Application.Current.MainWindow;

                if (mainWindow.DataContext is MainWindowModel mainViewModel)
                {
                    mainViewModel.CurrentViewModel = new ChatView();
                }
            }
            else
            {
                MessageBox.Show("Niepoprawny email lub hasło!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas logowania: {ex.Message}");
        }
    }
    

    private string EncryptPasswordLogin(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
