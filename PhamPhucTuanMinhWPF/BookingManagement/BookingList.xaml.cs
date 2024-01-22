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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        }
    }
}
