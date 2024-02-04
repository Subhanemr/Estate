using Twilio.Rest.Api.V2010.Account;

namespace Estate.Application.Abstractions.Services
{
    public interface ISmsSender
    {
        Task<MessageResource> SendSmsAsync(string phoneNumber, string message);
    }
}
