using BusinessObjects;
using Repositories;
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
using System.Windows.Shapes;

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
        public BookingReservation BookingReservation { get; set; } = new();

        public AddBooking(ICustomerRepository customerRepository, IDetailRepository detailRepository, IRoomRepository roomRepository)
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
                return;
            }
            var overlaps = _detailRepository
                .FindBookingDetails(det => det.StartDate <= dtTo.SelectedDate && dtFrom.SelectedDate <= det.EndDate)
                .Select(det => det.RoomId)
                .Distinct()
                .ToList();
            var availableRooms = _roomRepository.GetAllRooms();
            availableRooms.RemoveAll(room => overlaps.Contains(room.RoomId));
            dgRooms.ItemsSource = availableRooms;
            _startDate = (DateTime)dtFrom.SelectedDate;
            _endDate = (DateTime)dtTo.SelectedDate;
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = (RoomInformation)dgRooms.SelectedItem;
            int timeByDays = (int)(_endDate - _startDate).TotalDays;
            BookingDetail bookingDetail = new BookingDetail()
            {
                RoomId = roomInformation.RoomId,
                StartDate = _startDate,
                EndDate = _endDate,
                ActualPrice = roomInformation.RoomPricePerDay * timeByDays,
            };
            BookingReservation.BookingDetails.Add(bookingDetail);
            dgBookingDetail.ItemsSource = BookingReservation.BookingDetails;
            //todo: update price and available rooms
        }

        private void btnRemoveRoom_Click(object sender, RoutedEventArgs e)
        {
            BookingDetail bookingDetail = (BookingDetail)dgRooms.SelectedItem;
            BookingReservation.BookingDetails.Remove(bookingDetail);
            dgBookingDetail.ItemsSource = BookingReservation.BookingDetails;
            //todo: update price and available rooms
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //todo: business logic
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
