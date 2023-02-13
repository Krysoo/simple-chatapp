using System.Net.Sockets;

namespace ChatApp;

public class Server
{

    public void StartServer(string ip, int port)
    {
        
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