using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Http;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Dominio.Modelos;
using Twilio.Rest.Api.V2010.Account;

namespace Infraestructura.Repositorios
{
    public class SendSMS : ISendSMS
    {
        private readonly IHttpClientFactory _client;

        private readonly IConfiguration _config;

        public SendSMS(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }        

        public ITwilioRestClient CreateClient()
        {
            var httpClient = _client.CreateClient();
            return new TwilioRestClient(
                username: _config["Twilio:Account_SID"],
                password: _config["Twilio:Auth_Token"],
                httpClient: new SystemNetHttpClient(httpClient)
                );
        }

        public void SendMessage(TwilioSMS sms)
        {
            var message = MessageResource.Create(
                to: new Twilio.Types.PhoneNumber(sms.To),
                from: new Twilio.Types.PhoneNumber(sms.From),
                body: sms.Message,
                client: CreateClient()
                );
        }
    }
}
