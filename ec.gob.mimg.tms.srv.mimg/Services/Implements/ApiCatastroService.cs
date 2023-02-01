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
            baseUrl = root.GetSection("ApiMimg:url_api").Value + "ssn/ext/cc/Catastro/api/v1/";
            //...
            _logger = logger;
            _tokenService = tokenService;
        }



        public async Task<PredioApiResponse> GetPredio(PredioApiRequest request)
        {
            PredioApiResponse? response = new PredioApiResponse();
            PredioModel predio = new PredioModel();

            PredioInfoApiResponse? infoApiResponse = new PredioInfoApiResponse();
            PredioGpsApiResponse? gpsApiResponse = new PredioGpsApiResponse();

            // Se gestiona el token para ejecutar la consulta.
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenRequest tokenRequest = new TokenRequest();
            TokenResponse tokenResponse = await _tokenService.GetTokenTasa(tokenRequest);

            // Se realiza la consulta del contribuyente
            _logger.LogInformation(">>> GetContribuyente......{RunTime}", DateTime.Now);
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            //var apiRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            StringBuilder apiQuery = new StringBuilder();
            apiQuery.Append("TramiteSimplificadoSTH/VerificarPredio?");
            apiQuery.Append(string.Format("IdSector={0}", request.IdSector));
            apiQuery.Append(string.Format("&Manzana={0}", request.Manzana));
            apiQuery.Append(string.Format("&Lote={0}", request.Lote));
            apiQuery.Append(string.Format("&Division={0}", request.Division));
            apiQuery.Append(string.Format("&Phv={0}", request.Phv));
            apiQuery.Append(string.Format("&Phh={0}", request.Phh));
            apiQuery.Append(string.Format("&Numero={0}", request.Numero));

            var apiResponse = await cliente.GetAsync(apiQuery.ToString());
            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                infoApiResponse = JsonConvert.DeserializeObject<PredioInfoApiResponse>(data);
                //...
                if (infoApiResponse != null)
                {
                    predio.Parroquia = infoApiResponse.DataResult[0].Parroquia;
                    predio.Ciudadela = infoApiResponse.DataResult[0].Ciudadela;
                    predio.UsoEdificacion = infoApiResponse.DataResult[0].UsoEdificacion;
                    //..
                    response.Resultado = infoApiResponse.Resultado;
                    response.DataResult = predio;
                }

                var gpsRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                StringBuilder gpsQuery = new StringBuilder();
                gpsQuery.Append("TramiteSimplificadoSTH/CoordenadasPredio?");
                gpsQuery.Append(string.Format("IdSector={0}", request.IdSector));
                gpsQuery.Append(string.Format("&Manzana={0}", request.Manzana));
                gpsQuery.Append(string.Format("&Lote={0}", request.Lote));
                gpsQuery.Append(string.Format("&Division={0}", request.Division));
                gpsQuery.Append(string.Format("&Phv={0}", request.Phv));
                gpsQuery.Append(string.Format("&Phh={0}", request.Phh));
                gpsQuery.Append(string.Format("&Numero={0}", request.Numero));
                var gpsResponse = await cliente.GetAsync(gpsQuery.ToString());
                //...
                if (gpsResponse.IsSuccessStatusCode)
                {
                    var gpsData = await gpsResponse.Content.ReadAsStringAsync();
                    gpsApiResponse = JsonConvert.DeserializeObject<PredioGpsApiResponse>(gpsData);
                    //...
                    if (gpsApiResponse != null)
                    {
                        predio.Latitud = gpsApiResponse.DataResult[0].Latitud;
                        predio.Longitud = gpsApiResponse.DataResult[0].Longitud;
                        //..
                        response.Resultado = gpsApiResponse.Resultado;
                        response.DataResult = predio;
                    }
                }
                else
                {
                    response.Resultado = ResultadoModel.NotFound();
                }
            }
            else
            {
                response.Resultado = ResultadoModel.NotFound();
            }

            return response;
        }


    }
}
