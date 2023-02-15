using System.Net;
using System.Net.Sockets;

namespace ChatApp;

public class Server
{

    public void StartServer(string ip, int port)
    {
        TcpListener server = null;
        try
        {

            IPAddress address = IPAddress.Parse(ip);

            server = new TcpListener(address, port);

            server.Start();

            Byte[] bytes = new byte[256];
            String data = null;

            //nasłuchiwanie
            while (true)
            {
                Console.WriteLine("Trwa lączenie z serwerem...");
                using TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Połaczono!");

                data = null;

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
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
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            server.Stop();
        }
        Console.WriteLine("Wciśnij enter aby kontynuowac");
        Console.Read();
    }
    
    public bool IsServerAvailable(string ip, int port)
    {
        using (TcpClient client = new TcpClient())
        {
            try
            {
                client.Connect(ip, port);
            }
            catch (SocketException)
            {
                return false;
            }
            client.Close();
            return true;
        }
    }
}