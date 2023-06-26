using Aplicacion.Interfaces;
using Dominio.Interfaces;
using Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio
{
    public class MessageService : IMessageService
    {
        private ISendSMS repoSendSMS;

        public MessageService(ISendSMS sendSMS)
        {
            repoSendSMS = sendSMS;
        }      

        async Task IMessageService.SendMessage(TwilioSMS sms)
        {
             repoSendSMS.SendMessage(sms);
        }
    }
}
