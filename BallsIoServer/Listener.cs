using SocketLibrary;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace BallsIoServer
{
    public class Listener
    {
        private const string _magicString = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        private readonly SocketListener _socket;

        public Listener()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            _socket = new SocketListener(10000, ipAddress.ToString());
            Console.WriteLine(_socket.UnderlyingSocket.LocalEndPoint);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ConnectedSocket player = _socket.Accept();
                    Console.WriteLine("Client added");
                    string message = player.Receive();
                    Console.WriteLine(message);
                    string reply = "Welcome!";

                    if (message.Contains("Sec-WebSocket-Key: "))
                    {
                        string key = message.Split(new string[] { "Sec-WebSocket-Key: " }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[0] + _magicString;
                        var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(key));
                        var accept = Convert.ToBase64String(hash);
                        reply = "HTTP/1.1 101 Switching Protocols\r\nUpgrade: websocket\r\nConnection: Upgrade\r\nSec-WebSocket-Accept: " + accept + "\r\n";
                    }
                    Console.WriteLine(reply);
                    player.Send(reply);
                    ClientAdded?.Invoke(player, message);
                }
            });            
        }

        public event Action<ConnectedSocket, string> ClientAdded;
    }
}
