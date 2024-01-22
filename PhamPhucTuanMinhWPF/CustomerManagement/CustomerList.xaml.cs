using BusinessObjects;
using Repositories;
using System.Windows;
using System.Windows.Controls;

namespace PhamPhucTuanMinhWPF.CustomerManagement
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Page
    {
        private readonly ICustomerRepository _repository;

        public CustomerList(ICustomerRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            LoadList();
        }

        private void LoadList()
        {
            var contents = _repository.GetAllCustomers();
            dgList.ItemsSource = contents;
        }

        private void btnViewInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.SelectedItem is not Customer)
            {
                return;
            }
            Customer customer = (Customer)dgList.SelectedItem;
            CustomerDetails customerDetails = new()
            {
                Customer = customer,
                Mode = Enums.WindowMode.View
            };
            customerDetails.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new();
            CustomerDetails customerDetails = new()
            {
                Customer = customer,
                Mode = Enums.WindowMode.Add
            };
            if (customerDetails.ShowDialog() ?? false)
            {
                _repository.AddCustomer(customer);
                LoadList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.SelectedItem is not Customer)
            {
                return;
            }
            Customer customer = (Customer)dgList.SelectedItem;
            CustomerDetails customerDetails = new()
            {
                Customer = customer,
                Mode = Enums.WindowMode.Edit
            };
            if (customerDetails.ShowDialog() ?? false)
            {
                _repository.UpdateCustomer(customer);
                LoadList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.SelectedItem is not Customer)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Delete confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Customer customer = (Customer)dgList.SelectedItem;
                _repository.DeleteCustomer(customer.CustomerId);
                LoadList();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var contents = _repository.FindCustomersByName(txtSearch.Text);
            dgList.ItemsSource = contents;
        }
    }
}
