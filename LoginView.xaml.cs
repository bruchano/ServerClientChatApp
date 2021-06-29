using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pie.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public static readonly DependencyProperty LoginCommandProperty =
            DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));

        public ICommand LoginCommand
        {
            get
            {
                return (ICommand)GetValue(LoginCommandProperty);
            }
            set
            {
                SetValue(LoginCommandProperty, value);
            }
        }

        public static readonly DependencyProperty PasswordGotFocusCommandProperty =
            DependencyProperty.Register("PasswordGotFocusCommand", typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));

        public ICommand PasswordGotFocusCommand
        {
            get
            {
                return (ICommand)GetValue(PasswordGotFocusCommandProperty);
            }
            set
            {
                SetValue(PasswordGotFocusCommandProperty, value);
            }
        }

        public LoginView()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string password = pbPassword.Password;
            LoginCommand.Execute(password);
        }

        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordGotFocusCommand.Execute(pbPassword);
        }
    }
}
