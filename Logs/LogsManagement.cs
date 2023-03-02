namespace Logs;

public class LogsManagement
{
    private List<Log> logs = new();

    public List<Log> GetLogs()
    {
        return logs;
    }

    public void SaveLogs()
    {
        using (StreamWriter sw = File.AppendText(DateTime.Now + ".txt"))
        {
            foreach (var log in logs)
            {
                sw.WriteLine(log.data.ToString() + " " + log.type + " " + log.message);
            }
        }
    }
    
    
}