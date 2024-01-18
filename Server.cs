using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    internal class Server
    {
        private static bool exit = false;

        public static void AcceptMessage()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
            UdpClient udpClient = new UdpClient(ipEndPoint);
            Console.WriteLine("Сервер ожидает сообщения...");

            Task exitServer = Task.Run(() =>
            {
                Console.ReadLine();
                exit = true;
               
            });

            while (!exit)
            {
                byte[] buffer = udpClient.Receive(ref ipEndPoint);
                string data = Encoding.UTF8.GetString(buffer);

                Task.Run(() =>
                {
                    Message msg = Message.FromJson(data);
                    Console.WriteLine(msg.ToString());
                    Message responseMsg = new Message("Server", "Сообщение получено!");
                    string responseMsgJs = responseMsg.ToJson();
                    byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                    udpClient.Send(responseDate, ipEndPoint);
                });
                exitServer.Wait();
            }
            
        }
    }
}
