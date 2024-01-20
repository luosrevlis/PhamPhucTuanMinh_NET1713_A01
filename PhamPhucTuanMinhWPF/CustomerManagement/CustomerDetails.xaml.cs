using BusinessObjects;
using PhamPhucTuanMinhWPF.Enums;
using System.Windows;

namespace PhamPhucTuanMinhWPF.CustomerManagement
{
    /// <summary>
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        public WindowMode Mode { get; set; } = WindowMode.View;
        public Customer Customer { get; set; } = new Customer();

        public CustomerDetails()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Customer.CustomerFullName = txtFullName.Text;
            Customer.Telephone = txtPhone.Text;
            Customer.EmailAddress = txtEmail.Text;
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = Customer.CustomerFullName;
            txtPhone.Text = Customer.Telephone;
            txtEmail.Text = Customer.EmailAddress;
            if (Mode == WindowMode.View)
            {
                txtFullName.IsEnabled = false;
                txtPhone.IsEnabled = false;
                txtEmail.IsEnabled = false;
            }
        }
    }
}
