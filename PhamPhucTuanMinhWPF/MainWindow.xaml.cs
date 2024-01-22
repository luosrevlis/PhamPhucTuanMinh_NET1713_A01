using PhamPhucTuanMinhWPF.BookingManagement;
using PhamPhucTuanMinhWPF.CustomerManagement;
using PhamPhucTuanMinhWPF.RoomManagement;
using System.Windows;

namespace PhamPhucTuanMinhWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BookingList _bookingList;
        private readonly RoomList _roomList;
        private readonly CustomerList _customerList;

        public MainWindow(BookingList bookingList, RoomList roomList, CustomerList customerList)
        {
            InitializeComponent();
            _bookingList = bookingList;
            _roomList = roomList;
            _customerList = customerList;
            frMain.Content = bookingList;
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = _bookingList;
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = _roomList;
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = _customerList;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}