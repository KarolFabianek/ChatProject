using ChatAppGUI.Core;
using ChatAppGUI.MVVM.View;

namespace ChatAppGUI.MVVM.ViewModel;

public class MainWindowModel : ObservableObject
{
    private object _currentViewModel;
    public object CurrentViewModel
    {
        get
        {
            return _currentViewModel;
        }
        set
        {
            _currentViewModel = value;
            OnPropertyChanged("CurrentViewModel");
        }
    }

    public MainWindowModel()
    {
        CurrentViewModel = new ChatStartView();
    }
}