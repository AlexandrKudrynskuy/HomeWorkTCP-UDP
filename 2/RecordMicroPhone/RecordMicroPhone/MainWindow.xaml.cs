using NAudio.Wave;
using System;
using System.Collections.Generic;
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

namespace RecordMicroPhone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveIn waveIn;
        private WaveFileWriter writer;
        private string outputFilename= Guid.NewGuid()+"1.wav";
        private UdpClient server;
        private IPEndPoint endPoint;

        public MainWindow()
        {
            InitializeComponent();
            server = new UdpClient();
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.255"),2424);
        }

        private void WaveIn_RecordingStopped(object? sender, StoppedEventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;

        }

        private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
        {
           
                var res = e.Buffer;
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);


        }
        private void RecordButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                waveIn = new WaveIn();
           
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += WaveIn_DataAvailable; ;
                waveIn.RecordingStopped += WaveIn_RecordingStopped; ;
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                RecordLabel.Content = "Start Recording";
                waveIn.StartRecording();
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message); 
            }
           
        }
        private void RecordButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            waveIn.StopRecording();

            if (waveIn != null)
            {
                RecordLabel.Content = "Recording finich";
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var record = File.ReadAllBytes(outputFilename);
            server.SendAsync(record, record.Length, endPoint);
        }
    }
}
