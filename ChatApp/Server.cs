using System.Net;
using System.Net.Sockets;
using Logs;

namespace ChatApp;

public class Server
{

    public void StartServer(string ip, int port, User user)
    {
        TcpListener server = null;
        try
        {

            IPAddress address = IPAddress.Parse(ip);

            server = new TcpListener(address, port);

            server.Start();

            LogsManagement lm = new LogsManagement();
            
            Byte[] bytes = new byte[256];
            String data = null;

            //nasłuchiwanie
            while (true)
            {
                using TcpClient client = server.AcceptTcpClient();

                data = null;

                NetworkStream stream = client.GetStream();
                
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    
                    lm.GetLogs().Add(new Log(DateTime.Now, user.Name, data));
                    
                    Console.WriteLine(user.Name + "@" + user.PCName + " >> " + data);

                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    stream.Write(msg, 0, msg.Length);
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