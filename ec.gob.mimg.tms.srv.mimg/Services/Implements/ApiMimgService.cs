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

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public class ApiMimgService : IApiMimgService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly ITokenService _tokenService;
        //...
        private static string? subscriptionKey;
        private static string? baseUrl;

        public ApiMimgService(ILogger<TokenService> logger, ITokenService tokenService)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            subscriptionKey = root.GetSection("ApiMimg:subscription_key").Value;
            baseUrl = root.GetSection("ApiMimg:url_api").Value + "ssn/ext/cc/TasaHabilitacion/api/";
            //...
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<FactibilidadUsoResponse> GetFacilidadUso(FactibilidadUsoRequest request)
        {
            FactibilidadUsoResponse? response = new FactibilidadUsoResponse();
          
            // Se gestiona el token para ejecutar la consulta.
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenRequest tokenRequest = new TokenRequest();
            TokenResponse tokenResponse = await _tokenService.GetTokenHabilitacion(tokenRequest);

            // Se realiza la consulta del contribuyente
            _logger.LogInformation(">>> GetContribuyente......{RunTime}", DateTime.Now);
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var apiRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            StringBuilder apiQuery = new StringBuilder();
            apiQuery.Append("TramiteSimplificadoSTH/FactibilidadUso?");
            apiQuery.Append(string.Format("IdActividad={0}", request.IdActividad));
            apiQuery.Append(string.Format("&IdSector={0}", request.IdSector));
            apiQuery.Append(string.Format("&Manzana={0}", request.Manzana));
            apiQuery.Append(string.Format("&Lote={0}", request.Lote));
            apiQuery.Append(string.Format("&Division={0}", request.Division));
            apiQuery.Append(string.Format("&Phv={0}", request.Phv));
            apiQuery.Append(string.Format("&Phh={0}", request.Phh));
            apiQuery.Append(string.Format("&Numero={0}", request.Numero));

            var apiResponse = await cliente.GetAsync(apiQuery.ToString());

            if (apiResponse!= null && apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<FactibilidadUsoResponse>(data);
            }
            else {
                response.Resultado = ResultadoModel.setError();
            }

            return response;
        }

        public async Task<DimensionesResponse> GetDimensionMinima(DimensionesRequest request)
        {
            DimensionesResponse? response = new DimensionesResponse();

            // Se gestiona el token para ejecutar la consulta.
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenRequest tokenRequest = new TokenRequest();
            TokenResponse tokenResponse = await _tokenService.GetTokenHabilitacion(tokenRequest);

            // Se realiza la consulta del contribuyente
            _logger.LogInformation(">>> GetDimensionMinima......{RunTime}", DateTime.Now);
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var apiRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            StringBuilder apiQuery = new StringBuilder();
            apiQuery.Append("TramiteSimplificadoSTH/DimensionMinimaActividad?");
            apiQuery.Append(string.Format("IdActividad={0}", request.IdActividad));
            apiQuery.Append(string.Format("&IdSector={0}", request.IdSector));
            apiQuery.Append(string.Format("&Manzana={0}", request.Manzana));
            apiQuery.Append(string.Format("&Lote={0}", request.Lote));
            apiQuery.Append(string.Format("&Division={0}", request.Division));
            apiQuery.Append(string.Format("&Phv={0}", request.Phv));
            apiQuery.Append(string.Format("&Phh={0}", request.Phh));
            apiQuery.Append(string.Format("&Numero={0}", request.Numero));

            var apiResponse = await cliente.GetAsync(apiQuery.ToString());

            if (apiResponse != null && apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<DimensionesResponse>(data);
            }
            else
            {
                response.Resultado = ResultadoModel.setError();
            }

            return response;
        }

        public async Task<PredioApiResponse> GetCatalogoActividades(PredioApiRequest request)
        {
            throw new NotImplementedException();
        }

        Task<PredioApiResponse> IApiMimgService.CreateTasaHabilitacion(PredioApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
