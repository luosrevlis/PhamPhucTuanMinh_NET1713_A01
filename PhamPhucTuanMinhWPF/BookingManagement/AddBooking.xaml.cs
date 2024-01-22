using BusinessObjects;
using Repositories;
using System.Windows;

namespace PhamPhucTuanMinhWPF.BookingManagement
{
    /// <summary>
    /// Interaction logic for AddBooking.xaml
    /// </summary>
    public partial class AddBooking : Window
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDetailRepository _detailRepository;
        private readonly IRoomRepository _roomRepository;
        private DateTime _startDate;
        private DateTime _endDate;
        private List<RoomInformation> _rooms = new();
        public BookingReservation BookingReservation { get; set; } = new();

        public AddBooking(
            ICustomerRepository customerRepository,
            IDetailRepository detailRepository,
            IRoomRepository roomRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _detailRepository = detailRepository;
            _roomRepository = roomRepository;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbCustomer.ItemsSource = _customerRepository.GetAllCustomers();
            cbCustomer.DisplayMemberPath = "CustomerFullName";
            cbCustomer.SelectedValuePath = "CustomerId";
        }

        private void btnSearchRooms_Click(object sender, RoutedEventArgs e)
        {
            if (dtFrom.SelectedDate == null || dtTo.SelectedDate == null)
            {
                MessageBox.Show("Start date and end date cannot be empty!", "Error");
                return;
            }
            if (MessageBox.Show("This action will clear booking details. Continue?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                return;
            }
            _startDate = (DateTime)dtFrom.SelectedDate;
            _endDate = (DateTime)dtTo.SelectedDate;
            var overlaps = _detailRepository
                .FindBookingDetails(det => det.StartDate <= _startDate && _endDate <= det.EndDate)
                .Select(det => det.RoomId)
                .Distinct()
                .ToList();
            _rooms = _roomRepository.GetAllRooms();
            _rooms.RemoveAll(room => overlaps.Contains(room.RoomId));
            dgRooms.ItemsSource = _rooms;

            BookingReservation.BookingDetails = new List<BookingDetail>();
            dgBookingDetail.ItemsSource = BookingReservation.BookingDetails;
            BookingReservation.TotalPrice = 0;
            lbPrice.Content = "Total price:\n0";
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is not RoomInformation)
            {
                return;
            }
            RoomInformation roomInformation = (RoomInformation)dgRooms.SelectedItem;
            int timeByDays = (int)(_endDate - _startDate).TotalDays;
            BookingDetail bookingDetail = new()
            {
                RoomId = roomInformation.RoomId,
                StartDate = _startDate,
                EndDate = _endDate,
                ActualPrice = roomInformation.RoomPricePerDay * timeByDays,
                Room = roomInformation
            };
            BookingReservation.BookingDetails.Add(bookingDetail);
            dgBookingDetail.ItemsSource = null;
            dgBookingDetail.ItemsSource = BookingReservation.BookingDetails;

            _rooms.Remove(roomInformation);
            dgRooms.ItemsSource = null;
            dgRooms.ItemsSource = _rooms;
            BookingReservation.TotalPrice += bookingDetail.ActualPrice;
            lbPrice.Content = "Total price:\n" + BookingReservation.TotalPrice;
        }

        private void btnRemoveRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgBookingDetail.SelectedItem is not BookingDetail)
            {
                return;
            }
            BookingDetail bookingDetail = (BookingDetail)dgBookingDetail.SelectedItem;
            BookingReservation.BookingDetails.Remove(bookingDetail);
            dgBookingDetail.ItemsSource = null;
            dgBookingDetail.ItemsSource = BookingReservation.BookingDetails;

            _rooms.Add(bookingDetail.Room);
            dgRooms.ItemsSource = null;
            dgRooms.ItemsSource = _rooms;
            BookingReservation.TotalPrice -= bookingDetail.ActualPrice;
            lbPrice.Content = "Total price:\n" + BookingReservation.TotalPrice;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            BookingReservation.Customer = (Customer)cbCustomer.SelectedItem;
            BookingReservation.BookingDate = DateTime.UtcNow;
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
