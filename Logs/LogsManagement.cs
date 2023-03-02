namespace Logs;

public class LogsManagement
{
    private List<Log> logs = new();

    public List<Log> GetLogs()
    {
        return logs;
    }

    public void SaveLogs(DateTime now)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamWriter sw = File.AppendText(path + @"\ChatAppLogs\" +  now.Day + "-" + now.Month + "-" + now.Year + "-" + now.Hour + "-" + now.Minute + "-" + now.Second + ".txt"))
        {
            foreach (var log in logs)
            {
                sw.WriteLine(log.data.ToString() + log.type + " " + log.message);
            }
        }
    }
    
    
}