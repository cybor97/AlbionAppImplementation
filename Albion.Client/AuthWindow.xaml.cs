using Albion.Data;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace Albion.Client
{
    public partial class AuthWindow : Window
    {
        public Account Account => App.Account;
        bool _signUpMode = false;

        public bool SignUpMode
        {
            get { return _signUpMode; }
            set
            {
                _signUpMode = value;
                SignUpContainer.Visibility = _signUpMode ? Visibility.Visible : Visibility.Collapsed;
                Height = _signUpMode ? 550 : 200;
                SubmitButton.Content = Title = _signUpMode ? "Регистрация" : "Вход";
                SwitchModeButton.Content = _signUpMode ? "Уже зарегистрированы?" : "Не зарегистрированы?";
            }
        }
        public AuthWindow()
        {
            InitializeComponent();
            DataContext = Account;
        }

        private void SwitchModeButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpMode = !SignUpMode;
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account.Password = PasswordTB.Password;
                var (StatusCode, Body) = await NetworkTools.PostAsync(_signUpMode ? "/Auth/SignUp" : "/Auth/SignIn", Account);
                if (StatusCode == HttpStatusCode.OK)
                {
                    DialogResult = true;
                    App.Account = Body;
                }
                else MessageBox.Show("Неправильно указана электронная почта или пароль!");
            }
            catch (HttpRequestException exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}
