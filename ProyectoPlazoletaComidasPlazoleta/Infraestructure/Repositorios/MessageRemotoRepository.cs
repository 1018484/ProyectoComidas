using Dominio.Modelos;
using Dominio.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infraestructure.Repositorios
{
    /// <summary>
    /// Message Repository httpClient   
    /// </summary>  
    public class MessageRemotoRepository : IMessageRemotoRepository
    {
        /// <summary>
        /// Http intance  
        /// </summary>
        private readonly IHttpClientFactory _httpClient;

        /// <summary>
        /// Initialize Http intance  
        /// </summary>
        /// <param name="httpClient">httpClient.</param>
        public MessageRemotoRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Post send Message 
        /// </summary>
        /// <param name="sms">Message</param>
        public async Task SendMessageAsync(TWMessage sms)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Mensageria");                
                var content = JsonConvert.SerializeObject(sms);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await cliente.PostAsync ($"/api/SMS/", byteContent);              

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
