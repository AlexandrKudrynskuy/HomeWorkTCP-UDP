using MeNewClient.Model;
using MeNewClient.Service;
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
using static System.Net.Mime.MediaTypeNames;

namespace MeNewClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyClient client;
        public string Author { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            client = new MyClient("127.0.0.1", 3389);
        }

        private void AuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            Author = AuthorTextBox.Text;
            AuthorTextBox.Visibility = Visibility.Collapsed;
            AutTextBlock.Visibility = Visibility.Collapsed;
            AuthorBtn.Visibility = Visibility.Collapsed;
            MesTextBox.Visibility = Visibility.Visible;
            SendBtn.Visibility = Visibility.Visible;
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            var mess = new Message { Author = Author, Text = MesTextBox.Text };
            client.Send(mess);
            MesTextBox.Text = "";
        }
    }
}
