using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace ChatServer;

public class DataValidation
{
    private string _password;

    public string Password
    {
        get { return _password; }
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public bool CredentialsValidation()
    {
        if (string.IsNullOrEmpty(Password))
        {
            return false;
        }

        if (Password.Length < 8 & Password.Length > 50)
        {
            return false;
        }
            
        if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$"))
        {
            return false;
        }
            
        else
        {
            return true;
        }
    }

    public void PasswordEncryption(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            Password = BitConverter.ToString(hash).Replace("-", "").ToLower();
            
        }
        return;
    }
}