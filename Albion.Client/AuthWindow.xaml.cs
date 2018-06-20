using System.Windows;

namespace Albion.Client
{
    public partial class AuthWindow : Window
    {
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
        }

        private void SwitchModeButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpMode = !SignUpMode;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //Execute SignIn or SignUp method here
            DialogResult = true;
            Close();
        }
    }
}
