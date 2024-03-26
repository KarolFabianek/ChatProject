using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using ChatClient;

public class RegisterControlModel : INotifyPropertyChanged
{
    public ICommand RegisterCommand { get; private set; }
    public ICommand LoginCommand { get; private set; }

    public RegisterControlModel()
    {
        
    }
    // public RegisterControlModel()
    // {
    //     RegisterCommand = new RelayCommand(Register);
    //     LoginCommand = new RelayCommand(Login);
    // }

    // private void Register(object parameter)
    // {
    //     var newUser = (Client)parameter;
    //
    //     using (var dbContext = new YourDbContext())
    //     {
    //         // Sprawdzamy, czy użytkownik już istnieje w bazie danych
    //         var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == newUser.Username);
    //
    //         if (existingUser != null)
    //         {
    //             // Jeśli użytkownik już istnieje, możemy obsłużyć ten przypadek
    //             // Na przykład możemy zwrócić komunikat o błędzie lub podjąć inną akcję
    //             // w zależności od potrzeb
    //             Console.WriteLine("Użytkownik o podanej nazwie już istnieje.");
    //             return;
    //         }
    //
    //         // Jeśli użytkownik nie istnieje, możemy go dodać do bazy danych
    //         dbContext.Users.Add(newUser);
    //         dbContext.SaveChanges();
    //
    //         Console.WriteLine("Użytkownik został zarejestrowany.");
    //     }
    // }
    //
    // private void Login(object parameter)
    // {
    //     var loginData = (LoginData)parameter;
    //
    //     using (var dbContext = new YourDbContext())
    //     {
    //         var client = dbContext.Users.FirstOrDefault(c => c.Nick == loginData.Nick && c.Haslo == loginData.Haslo);
    //
    //         if (client != null)
    //         {
    //             Console.WriteLine("Zalogowano pomyślnie.");
    //         }
    //         else
    //         {
    //             Console.WriteLine("Błąd logowania. Sprawdź nazwę użytkownika i hasło.");
    //         }
    //     }
    // }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

    public void Execute(object parameter) => _execute(parameter);
}
