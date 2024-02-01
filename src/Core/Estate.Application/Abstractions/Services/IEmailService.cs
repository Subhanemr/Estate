namespace Estate.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(string emailTo, string subject, string body, bool isHtml = false);
    }
}
