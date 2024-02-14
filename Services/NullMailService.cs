using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> logger;

        public NullMailService(ILogger<NullMailService> _logger)
        {
            logger = _logger;
        }
        public void SendMessage(string to, string subject, string body)
        {

            logger.LogInformation($"Кому: {to}, Тема: {subject}, письмо: {body}");
        }
    }
}
