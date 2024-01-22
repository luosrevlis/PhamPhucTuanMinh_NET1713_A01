using DAOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhamPhucTuanMinhWPF.BookingManagement;
using PhamPhucTuanMinhWPF.CustomerManagement;
using PhamPhucTuanMinhWPF.RoomManagement;
using Repositories;
using Repositories.Impl;
using System.Configuration;
using System.Data;
using System.IO;
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
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            services.AddSingleton(configuration);
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Login>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<CustomerList>();
            services.AddSingleton<RoomList>();
            services.AddSingleton<BookingList>();
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
            var login = serviceProvider.GetService<Login>()!;
            login.Show();
        }
    }
}