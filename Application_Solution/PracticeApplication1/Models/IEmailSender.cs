namespace PracticeApplication1.Models
{
    public interface IEmailSender
    {
        void SendEmail(string recieverEmail, string subject, string body);
    }
}
