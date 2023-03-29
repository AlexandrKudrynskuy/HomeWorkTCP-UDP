using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCPServer.Model;
using TCPServer.Service;

namespace TCPServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server server;
        //public ObservableCollection<Message> Messages { get;set;}
        public MainWindow(Server _myServer, ObservableCollection<Message> _Messages)
        {
            InitializeComponent();
            this.Show();
            server = _myServer;
            //Messages = _Messages;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            server = new Server();
            server.ConfigureServer("127.0.0.1", 3389);
            server.StartServerAsync();
            server.ShowMessage += Server_ShowMessage;
            //MesListViev.ItemsSource = Messages;
        }

        private async void Server_ShowMessage(Model.Message obj)
        {
            //Messages.Add(obj);
            await Task.Run(() =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MesTextBox.Text += obj.Author + " " + obj.Text + "\n";
                    MesTextBox.UpdateLayout();
                });
            });
        } 
    
    }
}
