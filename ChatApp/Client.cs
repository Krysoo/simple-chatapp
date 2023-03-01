using System.Net.Sockets;
using Logs;

namespace ChatApp;

public class Client
{
    public void ConnectToServer(string ip, int port, User user)
    {
        try
        {
            while (true)
            {
                using TcpClient client = new TcpClient(ip, port);

                string message = Console.ReadLine();

                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();
                LogsManagement lm = new LogsManagement();
                
                lm.GetLogs().Add(new Log(DateTime.Now, user.Name, data));
                
                stream.Write(data, 0, data.Length);

                Console.WriteLine(user.Name + "@" + user.PCName + " >> " + message);

                data = new byte[256];

                string responseData = String.Empty;

                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        Console.WriteLine("\n Wcisnij enter aby kontynuowac");
        Console.Read();
    }
}