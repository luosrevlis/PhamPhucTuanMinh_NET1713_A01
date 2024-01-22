using Microsoft.Extensions.Configuration;
using System.Windows;

namespace PhamPhucTuanMinhWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly MainWindow _main;
        private readonly IConfiguration _configuration;

        public Login(MainWindow main, IConfiguration configuration)
        {
            InitializeComponent();
            _main = main;
            _configuration = configuration;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = _configuration.GetSection("AdminAccount")["Username"] ?? string.Empty;
            string password = _configuration.GetSection("AdminAccount")["Password"] ?? string.Empty;
            if (!username.Equals(txtUsername.Text) || !password.Equals(txtPassword.Password))
            {
                MessageBox.Show("Incorrect username or password!", "Error");
                return;
            }
            _main.Show();
            Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
