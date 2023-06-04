using ec.gob.mimg.tms.srv.mail.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace ec.gob.mimg.tms.srv.mail.Services.Implements
{

    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;

        private static string? XSenderID;
        private static string? XApikey;
        private static string? XMethod;
        private static string? BaseUrl;

        public MailService(ILogger<MailService> logger)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            XSenderID = root.GetSection("MailSettings:X-SenderID").Value;
            XApikey = root.GetSection("MailSettings:X-Apikey").Value;
            XMethod = root.GetSection("MailSettings:X-Method").Value;
            BaseUrl = root.GetSection("MailSettings:url").Value;
            _logger = logger;
        }

        public async Task<MailResponse> SendMail(MailRequest mailRequest)
        {
            _logger.LogError(">>> Enviando Mail......{RunTime}", DateTime.Now);

            MailResponse? mailResponse = new MailResponse();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(BaseUrl);
            cliente.DefaultRequestHeaders.Add("X-SenderID", XSenderID);
            cliente.DefaultRequestHeaders.Add("X-Apikey", XApikey);
            cliente.DefaultRequestHeaders.Add("X-Method", XMethod);

            var apiRequest = new StringContent(JsonConvert.SerializeObject(mailRequest), Encoding.UTF8, "application/json");
            var apiResponse = await cliente.PostAsync("ews/", apiRequest);

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                List<SendResponse>? list = JsonConvert.DeserializeObject<List<SendResponse>>(data);
                mailResponse.mailResponseList = list;
            }
            else
            {
                mailResponse = null;
            }

            return mailResponse;
        }
    }
}
