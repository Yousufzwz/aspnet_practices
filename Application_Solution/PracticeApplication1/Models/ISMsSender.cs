namespace PracticeApplication1.Models;

public interface ISMsSender
{
    void SendSMS(string mobile, string message);
}
