using DAOs;
using Microsoft.Extensions.DependencyInjection;
using PhamPhucTuanMinhWPF.CustomerManagement;
using PhamPhucTuanMinhWPF.RoomManagement;
using Repositories;
using Repositories.Impl;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PhamPhucTuanMinhWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<CustomerList>();
            services.AddSingleton<RoomList>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDetailRepository, DetailRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
            services.AddSingleton<CustomerDAO>();
            services.AddSingleton<DetailDAO>();
            services.AddSingleton<ReservationDAO>();
            services.AddSingleton<RoomDAO>();
            services.AddSingleton<RoomTypeDAO>();
            services.AddDbContext<FuMiniHotelManagementContext>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>()!;
            mainWindow.Show();
        }
    }
}