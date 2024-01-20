using DAOs;
using Microsoft.Extensions.DependencyInjection;
using PhamPhucTuanMinhWPF.CustomerManagement;
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
            services.AddSingleton<CustomerDetails>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddSingleton<CustomerDAO>();
            services.AddSingleton<RoomDAO>();
            services.AddSingleton<ReservationDAO>();
            services.AddDbContext<FuMiniHotelManagementContext>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>()!;
            mainWindow.Show();
        }
    }
}