namespace Logs;

public class Log
{
    public DateTime data { get; set; }
    // type = client/server
    public string type { get; set; }
    public string message { get; set; }

    public Log(DateTime data, string type, string message)
    {
        this.data = data;
        this.type = type;
        this.message = message;
    }
}