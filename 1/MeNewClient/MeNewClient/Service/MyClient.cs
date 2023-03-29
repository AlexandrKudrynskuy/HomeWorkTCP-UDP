using MeNewClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeNewClient.Service
{
    public class MyClient
    {
        private TcpClient tcpClient;
        public MyClient(string ipAdress, int port)
        {
            tcpClient = new TcpClient(ipAdress, port);
        }
        public void Send(Message mess)
        {
            var sendMes = JsonSerializer.Serialize(mess);
            var bytes = Encoding.UTF8.GetBytes(sendMes);
            tcpClient.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}
