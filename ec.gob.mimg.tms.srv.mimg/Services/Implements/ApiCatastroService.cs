using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public class ApiCatastroService : IApiCatastroService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly ITokenService _tokenService;

        //...
        private static string? subscriptionKey;
        private static string? baseUrl;

        public ApiCatastroService(ILogger<TokenService> logger, ITokenService tokenService)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            subscriptionKey = root.GetSection("ApiMimg:subscription_key").Value;
            baseUrl = root.GetSection("ApiMimg:url_api_catastro").Value;
            //...
            _logger = logger;
            _tokenService = tokenService;
        }



        public async Task<EstablecimientoApiResponse> GetPredio(EstablecimientoApiRequest request)
        {
            // Se gestiona el token para ejecutar la consulta.
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenRequest tokenRequest = new TokenRequest();
            TokenResponse tokenResponse = await _tokenService.GetToken(tokenRequest);

            // Se realiza la consulta del contribuyente
            _logger.LogInformation(">>> GetContribuyente......{RunTime}", DateTime.Now);
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var apiRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var apiResponse = await cliente.GetAsync(string.Format("TramiteSimplificadoSTH/VerificarPredio?IdSector=90&Manzana=1143&Lote=19&Division=0&Phv=0&Phh=0&Numero=1"));

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }

            throw new NotImplementedException();
        }

        Task<ContribuyenteApiResponse> IApiCatastroService.GetPredioInfo(ContribuyenteApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
