using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TCPServer.Model;
using TCPServer.Service;

namespace TCPServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider provider; 

        public App()
        {
            var service = new ServiceCollection();
            ConfigureService(service);
            provider = service.BuildServiceProvider();
        }
        public void ConfigureService(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<Server>();
            services.AddTransient<ObservableCollection<Message>>();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            provider.GetService<MainWindow>();

        }
    }
}
