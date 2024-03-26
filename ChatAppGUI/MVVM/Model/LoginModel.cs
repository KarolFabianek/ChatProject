using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatAppGUI.MVVM.Model
{
    public class LoginModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private bool _rememberMe;

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public bool RememberMe
        {
            get { return _rememberMe; }
            set
            {
                if (_rememberMe != value)
                {
                    _rememberMe = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public bool ValidateCredentials()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return false;
            
            if (Username.Length > 50)
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

        public void EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Password);
                byte[] hash = sha256.ComputeHash(bytes);
                Password = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
