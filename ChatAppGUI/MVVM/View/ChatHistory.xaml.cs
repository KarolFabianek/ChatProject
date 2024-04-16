using System.Windows;
using ChatAppGUI.MVVM.ViewModel;
using DotNetty.Transport.Channels;
using Microsoft.Win32;

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
            MessageBox.Show("Oceniono stronę na " + stars.ToString() +
                            " gwiazdek - zostań jeszcze chwile przekonamy Cie ze jest spoko");
        }

        if (stars == 1)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() +
                            " gwiazdke - Jeden to nasz numer w rankingu Chatów w Polsce. Napewno zle kliknales!");
        }

        if (stars == 2)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() +
                            " gwiazdki - 2 to bedziesz mial z egzaminu jak nas tak ocenisz");
        }

        if (stars == 3)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() + " gwiazdki- Jest 3 jest stabilnie");
        }

        if (stars == 4)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() +
                            " gwiazdki - Za słabo przeciagnales, jeszcze troche w prawo");
        }

        if (stars == 5)
        {
            MessageBox.Show("Oceniono stronę na " + stars.ToString() +
                            " gwiazdek - Jestes Prezesem, witamy w naszym elitarnym gronie");
        }
    }
}