using ChatApp;

namespace TestProject1;

public class UnitTest1
{
    // test nalezy wykonac po uruchomieniu programu!
    [Fact]
    public void ClientTest()
    {
        Server s = new();
        Assert.Equal(s.IsServerAvailable("127.0.0.1", 13000), true);
    }
}