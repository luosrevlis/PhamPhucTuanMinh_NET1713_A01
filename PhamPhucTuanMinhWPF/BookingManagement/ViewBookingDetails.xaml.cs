using BusinessObjects;
using System.Windows;

namespace PhamPhucTuanMinhWPF.BookingManagement
{
    /// <summary>
    /// Interaction logic for ViewBookingDetails.xaml
    /// </summary>
    public partial class ViewBookingDetails : Window
    {
        public ViewBookingDetails(BookingReservation res)
        {
            InitializeComponent();
            txtCustomer.Text = res.Customer.CustomerFullName;
            dtFrom.SelectedDate = res.BookingDetails.First().StartDate;
            dtTo.SelectedDate = res.BookingDetails.First().EndDate;
            txtPrice.Text = res.TotalPrice.ToString();
            dgBookingDetail.ItemsSource = res.BookingDetails;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
