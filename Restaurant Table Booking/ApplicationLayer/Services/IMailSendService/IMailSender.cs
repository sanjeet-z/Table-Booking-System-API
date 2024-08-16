namespace ApplicationLayer.Services.IMailSendService
{
    public interface IMailSender
    {
        void SendMailToUser(string email);
    }
}
