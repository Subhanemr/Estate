using Estate.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Estate.Infrastructure.Implementations
{
    public class SmsSender : ISmsSender
    {
        private readonly IConfiguration _conf;

        public SmsSender(IConfiguration conf)
        {
            _conf = conf;
        }

        public async Task<MessageResource> SendSmsAsync(string phoneNumber, string message)
        {
            TwilioClient.Init(_conf["TwilioSettings:Sid"], _conf["TwilioSettings:Token"]);

            var result = await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_conf["TwilioSettings:Phone"]),
                to: new PhoneNumber(phoneNumber)
                );
            return result;
        }
    }
}
