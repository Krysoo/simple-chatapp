namespace ChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Podaj swój nick: ");
            string nickname = Convert.ToString(Console.ReadLine());
            User user = new User(nickname);
            Console.WriteLine("Trwa sprawdzanie czy serwer jest dostępny..");
            Server server = new Server();
            bool isWorking = server.IsServerAvailable("127.0.0.1", 13000);
            if (!isWorking)
            {
                Console.WriteLine("Aktualnie nie jest utworzony serwer, trwa tworzenia...");
                server.StartServer("127.0.0.1", 13000, user);
            }
            else
            {
                Console.WriteLine("Trwa lączenie z serwerem...");
                Client client = new Client();
                client.ConnectToServer("127.0.0.1", 13000, user);
            }
        }
    }
}