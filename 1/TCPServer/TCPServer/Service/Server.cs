using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TCPServer.Model;

namespace TCPServer.Service
{
    public class Server
    {
        private TcpListener tcpListener;
        public event Action<Message> ShowMessage;
           public void ConfigureServer(string IpAddress, int port)
        {
            tcpListener = new TcpListener(IPAddress.Parse(IpAddress), port);
        }
        public async void StartServerAsync()
        {
            try
            {
                tcpListener.Start();
                var tempData = new Byte[1024];
                string data = null;
                while (true)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    int bytes = 0;
                    bool finich = false;
                    while ((bytes = tcpClient.GetStream().Read(tempData, 0, tempData.Length)) != 0)
                    {
                        data += Encoding.UTF8.GetString(tempData, 0, bytes);
                        if (tcpClient.Available <= 0)
                        {
                            var mess = JsonSerializer.Deserialize<Message>(data);
                            ShowMessage?.Invoke(mess);
                            finich = true;
                        }
                        if (finich)
                        {
                            data = null;
                        }
                    }

                 
                }
            }
            

            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                tcpListener.Stop();
            }
        }

    }
}
