using System.Windows;
using System.Windows.Controls;
using ChatAppGUI.MVVM.ViewModel;

namespace ChatAppGUI.MVVM.View;

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
}




//     RegisterCommand = new RelayCommand(Register);
//     LoginCommand = new RelayCommand(Login);
// }
//
// private void Register(object parameter)
// {
//     var registerData = parameter as RegisterData; // Klasa reprezentująca dane rejestracji, którą musisz zdefiniować
//     if (registerData == null)
//         return;
//
//     var newUser = new Client
//     {
//         Nickname = registerData.Nickname,
//         Password = registerData.Password,
//         Age = registerData.Age,
//         Email = registerData.Email;
//
//     };
//
//     using (var dbContext = new YourDbContext(DATABASE_CONNECTION_STRING))
//     {
//         // Sprawdzamy, czy użytkownik już istnieje w bazie danych
//         var existingUser = dbContext.Users.FirstOrDefault(n => n.Nickname == newUser.Nickname);
//
//         if (existingUser != null)
//         {
//             Console.WriteLine("Użytkownik o podanej nazwie już istnieje!");
//             return;
//         }
//         
//         dbContext.Users.Add(newUser);
//         dbContext.SaveChanges();
//
//         Console.WriteLine("Użytkownik został zarejestrowany.");
//     }
// }
//
// private void Login(object parameter)
// {
//
// }
// <TextBox Grid.Row="1" FontSize="17" Margin="110,10,10,0" Text="{Binding Username, UpdateSourceTrigger=PropertyChanged}"/>
// 
//
// 
// public class RegisterData
// {
//     public string Nickname { get; set; }
//     public int Age { get; set; }
//     public string Password { get; set; }
//     public string Email { get; set; }
// }  
