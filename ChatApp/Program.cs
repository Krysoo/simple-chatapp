namespace ChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Podaj swój nick: ");
            string nickname = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Trwa sprawdzanie czy serwer jest dostępny..");
            Server server = new Server();
            bool isWorking = server.IsServerAvailable("127.0.0.1", 123);
            if (!isWorking)
            {
                Console.WriteLine("Aktualnie nie jest utworzony serwer, trwa tworzenia...");
            }
        }
    }
}