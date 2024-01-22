using BusinessObjects;
using Repositories;
using System.Windows;
using System.Windows.Controls;

namespace PhamPhucTuanMinhWPF.RoomManagement
{
    /// <summary>
    /// Interaction logic for RoomList.xaml
    /// </summary>
    public partial class RoomList : Page
    {
        private readonly IRoomRepository _repository;
        private readonly IRoomTypeRepository _typeRepository;
        
        public RoomList(IRoomRepository repository, IRoomTypeRepository typeRepository)
        {
            InitializeComponent();
            _repository = repository;
            _typeRepository = typeRepository;
            LoadList();
        }

        private void LoadList()
        {
            var contents = _repository.GetAllRooms();
            dgList.ItemsSource = contents;
        }

        private void btnViewInfo_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = (RoomInformation)dgList.SelectedItem;
            RoomDetails roomDetails = new(_typeRepository)
            {
                Mode = Enums.WindowMode.View,
                RoomInformation = roomInformation
            };
            roomDetails.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = new();
            RoomDetails roomDetails = new(_typeRepository)
            {
                Mode = Enums.WindowMode.Add,
                RoomInformation = roomInformation
            };
            if (roomDetails.ShowDialog() ?? false)
            {
                _repository.AddRoom(roomInformation);
                LoadList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomInformation = (RoomInformation)dgList.SelectedItem;
            RoomDetails roomDetails = new(_typeRepository)
            {
                Mode = Enums.WindowMode.Edit,
                RoomInformation = roomInformation
            };
            if (roomDetails.ShowDialog() ?? false)
            {
                _repository.UpdateRoom(roomInformation);
                LoadList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this room?", "Delete confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                RoomInformation roomInformation = (RoomInformation)dgList.SelectedItem;
                _repository.DeleteRoom(roomInformation.RoomId);
            }
            LoadList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var contents = _repository.FindRooms(room =>
            {
                if (!string.IsNullOrEmpty(txtNumber.Text) && !room.RoomNumber.Contains(txtNumber.Text))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(txtDesc.Text) && !(room.RoomDetailDescription ?? string.Empty).Contains(txtDesc.Text))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(txtCapacity.Text) && (room.RoomMaxCapacity ?? 0) != int.Parse(txtCapacity.Text))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(txtPriceFrom.Text) && (room.RoomPricePerDay ?? 0) < int.Parse(txtPriceFrom.Text))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(txtPriceTo.Text) && (room.RoomPricePerDay ?? int.MaxValue) >  int.Parse(txtPriceTo.Text))
                {
                    return false;
                }
                return true;
            });
            dgList.ItemsSource = contents;
        }
    }
}
