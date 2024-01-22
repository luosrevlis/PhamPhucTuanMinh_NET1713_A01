using BusinessObjects;
using PhamPhucTuanMinhWPF.Enums;
using Repositories;
using System.Windows;

namespace PhamPhucTuanMinhWPF.CustomerManagement
{
    /// <summary>
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        private readonly ICustomerRepository _customerRepository;
        public WindowMode Mode { get; set; } = WindowMode.View;
        public Customer Customer { get; set; } = new Customer();

        public CustomerDetails(ICustomerRepository customerRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email address cannot be empty!", "Error");
                return;
            }
            if (_customerRepository.FindCustomerByEmail(txtEmail.Text) != null)
            {
                MessageBox.Show("Email already exists!", "Error");
                return;
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
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
