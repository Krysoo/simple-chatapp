using System.Net.Sockets;
using System.Text;
using Logs;

namespace Client {
    class ClientS
    {
        public void StartClient()
        {
            {
                try
                {
                    TcpClient client = new TcpClient("127.0.0.1", 13000);

                    Console.Clear();
                    Console.WriteLine("> Nawiązano połączenie z serwerem");

                    NetworkStream stream = client.GetStream();

                    while (true)
                    {
                        Console.Write("Client > ");
                        string message = Console.ReadLine();

                        bool doesSaveLogs = message.Equals("&savelogs;");
                        LogsManagement log = new();
                        
                        byte[] data = Encoding.ASCII.GetBytes(message);
                        stream.Write(data, 0, data.Length);

                        // odbieranie wiadomości
                        data = new byte[256];
                        StringBuilder response = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = stream.Read(data, 0, data.Length);
                            response.Append(Encoding.ASCII.GetString(data, 0, bytes));
                        } while (stream.DataAvailable);

                        Console.WriteLine("Server > " + response.ToString());
                        if (doesSaveLogs)
                        {
                            log.GetLogs().Add(new Log(DateTime.Now, "Client", message));
                            log.GetLogs().Add(new Log(DateTime.Now, "Server", response.ToString()));
                            log.SaveLogs();
                        }
                    }

                    stream.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
