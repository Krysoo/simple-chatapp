namespace Logs;

public class Log
{
    public DateTime data { get; set; }
    public string nickname { get; set; }
    public string message { get; set; }

    public Log(DateTime data, string nickname, string message)
    {
        this.data = data;
        this.nickname = nickname;
        this.message = message;
    }
}