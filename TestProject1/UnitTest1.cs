using Logs;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void LogsTest()
    {
        LogsManagement log = new LogsManagement();
        
        log.GetLogs().Add(new Log(DateTime.MaxValue, "Server", "XD"));
        
        Assert.Equal("XD", log.GetLogs().First().message);
        
    }
}