using Aplicacion.Interfaces;
using Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoPlazoletaMensajeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly IMessageService _messageService; 

        public SMSController(IMessageService serice)
        {
            this._messageService = serice;
        }

        [HttpPost]
        public void Post([FromBody] TwilioSMS sms)
        {
            _messageService.SendMessage(sms);
        }       
    }
}
