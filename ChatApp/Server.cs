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

                    Console.WriteLine("Aby zapisac logi tej wiadomosci uzyj komendy '&savelogs;'");
                    bool doesSaveLogs = Console.ReadLine().Equals("&savelogs;");

                    TcpListener listener = new TcpListener(IPAddress.Any, 13000);
                    listener.Start();

                    Console.Clear();
                    Console.WriteLine("> Uruchomiono połączenie");

                    DateTime now = DateTime.Now;

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

                            LogsManagement log = new LogsManagement();

                            data = Encoding.ASCII.GetBytes(response);
                            stream.Write(data, 0, data.Length);

                            if (doesSaveLogs)
                            {
                                log.GetLogs().Add(new Log(now, " Client:", message.ToString()));
                                log.GetLogs().Add(new Log(now, " Server:", response.ToString()));
                                log.SaveLogs(now);
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
                catch (IOException)
                {
                    Console.WriteLine("Client rozłączył się");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message );
                }
            }
        }
    }
}