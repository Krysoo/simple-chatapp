using System.Net;
using System.Net.Sockets;
using System.Text;
using Logs;


namespace ChatApp
{
    class Server
    {
        public void StartServer()
        {
            {
                try
                {
                    TcpListener listener = new TcpListener(IPAddress.Any, 13000);
                    listener.Start();

                    Console.Clear();
                    Console.WriteLine("> Uruchomiono połączenie");

                    // lista clientow
                    List<TcpClient> clients = new List<TcpClient>();

                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        clients.Add(client);

                        Console.Clear();
                        Console.WriteLine("> Połączono z klientem");
                        
                        NetworkStream stream = client.GetStream();

                        while (true)
                        {
                            byte[] data = new byte[256];
                            StringBuilder message = new StringBuilder();
                            int bytes = 0;
                            do
                            {
                                bytes = stream.Read(data, 0, data.Length);
                                message.Append(Encoding.ASCII.GetString(data, 0, bytes));
                            } while (stream.DataAvailable);

                            Console.WriteLine("Client > " + message.ToString());

                            Console.Write("Server > ");
                            string response = Console.ReadLine();

                            bool doesSaveLogs = message.Equals("&savelogs;");
                            LogsManagement log = new LogsManagement();
                            
                            data = Encoding.ASCII.GetBytes(response);
                            stream.Write(data, 0, data.Length);

                            if (doesSaveLogs)
                            {
                                log.GetLogs().Add(new Log(DateTime.Now, "Client", message.ToString()));
                                log.GetLogs().Add(new Log(DateTime.Now, "Server", response.ToString()));
                                log.SaveLogs();
                            }
                            
                            foreach (TcpClient c in clients)
                            {
                                if (c != client)
                                {
                                    stream = c.GetStream();
                                    data = Encoding.ASCII.GetBytes(message.ToString());
                                    stream.Write(data, 0, data.Length);
                                }
                            }
                        }

                        stream.Close();
                        client.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}