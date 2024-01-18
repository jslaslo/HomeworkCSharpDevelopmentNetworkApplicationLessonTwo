using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    internal class Client
    {
        public static void SendMessage(string name)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            UdpClient udpClient = new UdpClient();

           
            while (true)
            {
                Console.WriteLine("Введите сообщение. Если хотите выйти напишите exit");
                string text = Console.ReadLine();

                if (text.ToLower() == "exit")
                {
                    break;
                }

                Message msg = new Message(name, text);
                string responseMsgJs = msg.ToJson();
                byte[] resData = Encoding.UTF8.GetBytes(responseMsgJs);
                udpClient.Send(resData, endPoint);
                byte[] answerData = udpClient.Receive(ref endPoint);
                string aswerMsgJs = Encoding.UTF8.GetString(answerData);
                Message answerMsg = Message.FromJson(aswerMsgJs);
                Console.WriteLine(answerMsg.ToString());
            }
        }
    }
}
