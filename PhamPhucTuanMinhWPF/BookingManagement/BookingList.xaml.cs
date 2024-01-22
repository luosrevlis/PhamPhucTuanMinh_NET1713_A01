using BusinessObjects;
using Repositories;
using System.Windows;
using System.Windows.Controls;

namespace PhamPhucTuanMinhWPF.BookingManagement
{
    /// <summary>
    /// Interaction logic for BookingList.xaml
    /// </summary>
    public partial class BookingList : Page
    {
        private readonly IReservationRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDetailRepository _detailRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingList(
            IReservationRepository repository,
            ICustomerRepository customerRepository,
            IDetailRepository detailRepository,
            IRoomRepository roomRepository)
        {
            InitializeComponent();
            _repository = repository;
            _customerRepository = customerRepository;
            _detailRepository = detailRepository;
            _roomRepository = roomRepository;
            LoadList();
        }

        private void LoadList()
        {
            var contents = _repository.GetAllReservation();
            dgList.ItemsSource = contents;
        }

        private void btnViewInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.SelectedItem is not BookingReservation)
            {
                return;
            }
            BookingReservation res = (BookingReservation)dgList.SelectedItem; // todo check null
            res.BookingDetails = _detailRepository.FindBookingDetails(det => det.BookingReservationId == res.BookingReservationId);
            ViewBookingDetails view = new(res);
            view.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BookingReservation bookingReservation = new();
            AddBooking addBooking = new(_customerRepository, _detailRepository, _roomRepository)
            {
                BookingReservation = bookingReservation
            };
            if (addBooking.ShowDialog() ?? false)
            {
                _repository.AddReservation(bookingReservation);
                LoadList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.SelectedItem is not BookingReservation)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this reservation?", "Delete confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                BookingReservation bookingReservation = (BookingReservation)dgList.SelectedItem;
                _repository.DeleteReservation(bookingReservation.BookingReservationId);
                LoadList();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var contents = _repository.FindReservations(res =>
            {
                if (!string.IsNullOrEmpty(txtCustomer.Text) && !(res.Customer.CustomerFullName ?? string.Empty).Contains(txtCustomer.Text))
                {
                    return false;
                }
                if (dtBookingFrom.SelectedDate != null && res.BookingDate < dtBookingFrom.SelectedDate)
                {
                    return false;
                }
                if (dtBookingTo.SelectedDate != null && res.BookingDate > dtBookingTo.SelectedDate)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(txtPriceFrom.Text) && res.TotalPrice < int.Parse(txtPriceFrom.Text))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(txtPriceTo.Text) && res.TotalPrice >  int.Parse(txtPriceTo.Text))
                {
                    return false;
                }
                return true;
            });
            dgList.ItemsSource = contents;
        }
    }
}
