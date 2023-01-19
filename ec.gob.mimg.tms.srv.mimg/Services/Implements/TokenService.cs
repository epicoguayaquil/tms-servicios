using System;
using System.Text;
using System.Text.Json;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;

        private static string? clientId;
        private static string? scope;
        private static string? scope_tasa;
        private static string? clientSecret;
        private static string? grantType;
        //...
        private static string? BaseUrl;

        public TokenService(ILogger<TokenService> logger)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            clientId = root.GetSection("ApiMimg:client_id").Value;
            scope = root.GetSection("ApiMimg:scope").Value;
            scope_tasa = root.GetSection("ApiMimg:scope_tasa").Value;
            clientSecret = root.GetSection("ApiMimg:client_secret").Value;
            grantType = root.GetSection("ApiMimg:grant_type").Value;
            BaseUrl = root.GetSection("ApiMimg:url_token").Value;
            //...
            _logger = logger;
        }

        public async Task<TokenResponse> GetToken(TokenRequest request)
        {
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenResponse? tokenResponse = new TokenResponse();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(BaseUrl);

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", clientId));
            nvc.Add(new KeyValuePair<string, string>("scope", scope));
            nvc.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            nvc.Add(new KeyValuePair<string, string>("grant_type", grantType));

            var req = new HttpRequestMessage(HttpMethod.Post, BaseUrl) { Content = new FormUrlEncodedContent(nvc) };
            var apiResponse = await cliente.SendAsync(req);

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(data);
            }
            else
            {
                tokenResponse = null;
            }

            return tokenResponse;
        }

        public async Task<TokenResponse> GetTokenTasa(TokenRequest request)
        {
            _logger.LogInformation(">>> GetToken......{RunTime}", DateTime.Now);
            TokenResponse? tokenResponse = new TokenResponse();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(BaseUrl);

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", clientId));
            nvc.Add(new KeyValuePair<string, string>("scope", scope_tasa));
            nvc.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            nvc.Add(new KeyValuePair<string, string>("grant_type", grantType));

            var req = new HttpRequestMessage(HttpMethod.Post, BaseUrl) { Content = new FormUrlEncodedContent(nvc) };
            var apiResponse = await cliente.SendAsync(req);

            if (apiResponse.IsSuccessStatusCode)
            {
                var data = await apiResponse.Content.ReadAsStringAsync();
                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(data);
            }
            else
            {
                tokenResponse = null;
            }

            return tokenResponse;
        }
    }
}

