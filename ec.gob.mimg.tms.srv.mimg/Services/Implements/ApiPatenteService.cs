using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using ec.gob.mimg.tms.srv.mimg.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public class ApiPatenteService : IApiPatenteService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly ITokenService _tokenService;
        //...
        private static string? subscriptionKey;
        private static string? baseUrl;

        public ApiPatenteService(ILogger<TokenService> logger, ITokenService tokenService)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            subscriptionKey = root.GetSection("ApiMimg:subscription_key").Value;
            baseUrl = root.GetSection("ApiMimg:url_api").Value + "ssn/ext/cc/Patente/api/";
            //...
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<DeclaracionResponse> GetByRuc(ContribuyenteApiRequest request)
        {

            DeclaracionResponse? response = new DeclaracionResponse();

            // Se gestiona el token para ejecutar la consulta.
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenRequest tokenRequest = new TokenRequest();
            TokenResponse tokenResponse = await _tokenService.GetTokenPatente(tokenRequest);

            // Se realiza la consulta del contribuyente
            _logger.LogInformation(">>> GetByRuc......{RunTime}", DateTime.Now);
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var apiRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            StringBuilder apiQuery = new StringBuilder();
            apiQuery.Append(string.Format("Declaracion/ConsultarPorRuc/{0}", request.Ruc));
            
            var apiResponse = await cliente.GetAsync(apiQuery.ToString());

            if (apiResponse != null && apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<DeclaracionResponse>(data);
            }
            else
            {
                response.Resultado = ResultadoModel.setError();
            }

            return response;
        }
    }
}
