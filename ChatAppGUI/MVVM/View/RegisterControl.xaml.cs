using System;
using System.Windows;
using System.Windows.Controls;
using ChatAppGUI.MVVM.ViewModel;
using ChatProtocol.Packets;
using ChatServer;

namespace ChatAppGUI.MVVM.View
{
    public partial class RegisterControl : UserControl
    {
        public RegisterControl()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow.DataContext is MainWindowModel mainViewModel)
            {
                mainViewModel.CurrentViewModel = new LoginModel();
            }
        }

        private async void RegisterClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtEmail.Text;
                string password = txtPassword.Password;
                string nickname = txtNickname.Text;
                int age = int.Parse(txtAge.Text);

                var registerPacket = new ClientRegisterPacket()
                {
                    _password = password,
                    _nickname = nickname,
                    _age = age,
                    _email = email,
                    _id = Guid.NewGuid()
                };

                var loginModel = new Model.LoginModel();
                if (!loginModel.ValidateCredentials(email, password, nickname, age))
                {
                    MessageBox.Show("Podane dane są nieprawidłowe. Sprawdź formaty i długość danych.");
                    return;
                }

                string encryptedPassword = loginModel.EncryptPassword(password);

                var userCache = new UserCache();

                await userCache.ConnectToDatabaseAsync(email, encryptedPassword, nickname, registerPacket._id, age);

                MessageBox.Show("Rejestracja zakończona pomyślnie!");

                var mainWindow = Application.Current.MainWindow;

                if (mainWindow.DataContext is MainWindowModel mainViewModel)
                {
                    mainViewModel.CurrentViewModel = new LoginView();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadzono nieprawidłowy format danych.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas rejestracji: {ex.Message}");
            }
        }

    }
}
