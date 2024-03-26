using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ChatAppGUI.MVVM.ViewModel;

namespace ChatAppGUI.MVVM.View;
public partial class ChatStartView : UserControl
{
    public ChatStartView()
    {
        InitializeComponent();
    }


    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow1 = Application.Current.MainWindow;
            
        if (mainWindow1.DataContext is MainWindowModel mainViewModel)
        {
            mainViewModel.CurrentViewModel = new LoginModel();
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow2 = Application.Current.MainWindow;
        
        if (mainWindow2.DataContext is MainWindowModel mainViewModel)
        {
            mainViewModel.CurrentViewModel = new RegisterControlModel();
        }
    }
}