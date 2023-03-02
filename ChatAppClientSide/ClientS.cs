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
                    Console.WriteLine("Aby zapisac logi tej wiadomosci uzyj komendy '&savelogs;'");

                    bool doesSaveLogs = Console.ReadLine().Equals("&savelogs;");

                    TcpClient client = new TcpClient("127.0.0.1", 13000);

                    Console.Clear();
                    Console.WriteLine("> Nawiązano połączenie z serwerem");

                    DateTime now = DateTime.Now;

                    NetworkStream stream = client.GetStream();

                    while (true)
                    {
                        Console.Write("Client > ");
                        string message = Console.ReadLine();

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
                            log.GetLogs().Add(new Log(now, " Client:", message));
                            log.GetLogs().Add(new Log(now, " Server:", response.ToString()));
                            log.SaveLogs(now);
                        }
                    }

                    stream.Close();
                    client.Close();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Serwer nie dziala");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
