using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatServer
{
    class Server
    {
        public Server()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 13000;
                IPAddress localAddress = IPAddress.Parse("192.168.1.88");

                server = new TcpListener(localAddress, port);

                server.Start();

                Byte[] bytes = new Byte[256];
                string data = null;

                while (true)
                {
                    Console.WriteLine("Waiting...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i= stream.Read(bytes, 0, bytes.Length))!= 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
            Console.ReadKey(true);
        }
    }
}
