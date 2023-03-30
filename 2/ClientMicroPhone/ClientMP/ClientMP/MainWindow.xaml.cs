using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Controls.Button;
using Window = System.Windows.Window;

namespace ClientMP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> Records { get; set; }
        private NAudio.Wave.WaveFileReader wave;
        private NAudio.Wave.DirectSoundOut output;
        private UdpClient client;
        private IPEndPoint endPoint;
        public MainWindow()
        {
            InitializeComponent();
            wave = null;
            output = null;
            client = new UdpClient(2424);
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.255"), 2424);
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {

                wave = new NAudio.Wave.WaveFileReader(button.Tag.ToString());
                output = new NAudio.Wave.DirectSoundOut();
                output.Init(new NAudio.Wave.WaveChannel32(wave));
                output.Play();
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Records.Remove(button.Tag.ToString());
                System.IO.File.Delete(button.Tag.ToString());
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Records = new ObservableCollection<string>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + "record");
            foreach (var file in files)
            {
                Records.Add(file);
            }
            recordListView.ItemsSource = Records;


            await Task.Run(() =>
            {
            while (true)

            {
                byte[] res = client.Receive(ref endPoint);
                string path = Directory.GetCurrentDirectory() + "\\" + "record" + "\\" + Guid.NewGuid() + ".wav";
                System.IO.File.WriteAllBytes(path, res);
                Dispatcher.BeginInvoke(() => {
                    Records.Add(path);
                });

                }
            });





        }
    }
}
