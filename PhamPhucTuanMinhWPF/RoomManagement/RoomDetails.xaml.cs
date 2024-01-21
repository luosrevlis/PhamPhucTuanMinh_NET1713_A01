using BusinessObjects;
using PhamPhucTuanMinhWPF.Enums;
using Repositories;
using System.Windows;

namespace PhamPhucTuanMinhWPF.RoomManagement
{
    /// <summary>
    /// Interaction logic for RoomDetails.xaml
    /// </summary>
    public partial class RoomDetails : Window
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        public WindowMode Mode { get; set; } = WindowMode.View;
        public RoomInformation RoomInformation { get; set; } = new();

        public RoomDetails(IRoomTypeRepository roomTypeRepository)
        {
            InitializeComponent();
            _roomTypeRepository = roomTypeRepository;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation.RoomNumber = txtNumber.Text;
            RoomInformation.RoomMaxCapacity = int.Parse(txtCapacity.Text);
            RoomInformation.RoomPricePerDay = decimal.Parse(txtPrice.Text);
            RoomInformation.RoomType = (RoomType)cbType.SelectedItem;
            RoomInformation.RoomTypeId = RoomInformation.RoomType.RoomTypeId;
            RoomInformation.RoomDetailDescription = txtDesc.Text;
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
            cbType.ItemsSource = _roomTypeRepository.GetAllRoomTypes();
            cbType.DisplayMemberPath = "RoomTypeName";
            cbType.SelectedValuePath = "RoomTypeId";

            cbType.SelectedValue = RoomInformation.RoomTypeId;
            txtNumber.Text = RoomInformation.RoomNumber;
            txtCapacity.Text = RoomInformation.RoomMaxCapacity.ToString();
            txtPrice.Text = RoomInformation.RoomPricePerDay.ToString();
            txtDesc.Text = RoomInformation.RoomDetailDescription;
            if (Mode == WindowMode.View)
            {
                txtNumber.IsEnabled = false;
                txtCapacity.IsEnabled = false;
                txtPrice.IsEnabled = false;
                txtDesc.IsEnabled = false;
                cbType.IsEnabled = false;
            }
        }
    }
}
