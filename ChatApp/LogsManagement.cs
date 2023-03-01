namespace Logs;

public class LogsManagement
{
    private List<Log> logs = new();

    public List<Log> GetLogs()
    {
        return logs;
    }
}