﻿using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public class ApiSriService : IApiSriService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly ITokenService _tokenService;

        //...
        private static string? subscriptionKey;
        private static string? baseUrl;

        public ApiSriService(ILogger<TokenService> logger, ITokenService tokenService)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            subscriptionKey = root.GetSection("ApiMimg:subscription_key").Value;
            baseUrl = root.GetSection("ApiMimg:url_api").Value + "ssn/ext/cc/Sri/api/";
            //...
            _logger = logger;
            _tokenService = tokenService;
        }


        public async Task<ContribuyenteApiResponse> GetContribuyente(ContribuyenteApiRequest request)
        {
            ContribuyenteApiResponse? response = new ContribuyenteApiResponse();

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
            var apiResponse = await cliente.GetAsync(string.Format("Contribuyente/ConsultarPorRuc/{0}", request.Ruc ));

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ContribuyenteApiResponse>(data);
            }
            else
            {
                response = null;
            }

            return response;
        }

        public async Task<EstablecimientoApiResponse> GetEstablecimientos(EstablecimientoApiRequest request)
        {
            EstablecimientoApiResponse? response = new EstablecimientoApiResponse();

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
            var apiResponse = await cliente.GetAsync(string.Format("Establecimiento/ConsultarPorRuc/{0}", request.Ruc));


            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<EstablecimientoApiResponse>(data);
            }
            else
            {
                response = null;
            }

            return response;
        }

        public async Task<ActividadApiResponse> GetActividadEstablecimiento(ActividadApiRequest request)
        {
            ActividadApiResponse? response = new ActividadApiResponse();

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
            var apiResponse = await cliente.GetAsync(string.Format("Establecimiento/ConsultarActividades?Ruc={0}&Numero={1}", request.Ruc,request.Establecimiento));

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ActividadApiResponse>(data);
            }
            else
            {
                response = null;
            }

            return response;
        }

        public async Task<EstablecimientoApiResponse> GetEstablecimientosNuevos(string Fecha)
        {
            EstablecimientoApiResponse? response = new EstablecimientoApiResponse();

            // Se gestiona el token para ejecutar la consulta.
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenRequest tokenRequest = new TokenRequest();
            TokenResponse tokenResponse = await _tokenService.GetToken(tokenRequest);

            // Se realiza la consulta del contribuyente
            _logger.LogInformation(">>> GetEstablecimientosNuevos......{RunTime}", DateTime.Now);
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var apiRequest = new StringContent(JsonConvert.SerializeObject(Fecha), Encoding.UTF8, "application/json");
            var apiResponse = await cliente.GetAsync(string.Format("Establecimiento/ConsultarNuevos/{0}", Fecha));

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<EstablecimientoApiResponse>(data);
            }
            else
            {
                response = null;
            }

            return response;
        }
    }
}
