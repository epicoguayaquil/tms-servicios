using System;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
	public class TokenService : ITokenService
	{
        private readonly ILogger<TokenService> _logger;

        private static string? clientId;
        private static string? scope;
        private static string? clientSecret;
        private static string? grantType;

        public TokenService(ILogger<TokenService> logger)
		{
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var root = builder.Build();
            clientId = root.GetSection("MailSettings:X-SenderID").Value;
            scope = root.GetSection("MailSettings:X-Apikey").Value;
            clientSecret = root.GetSection("MailSettings:X-Method").Value;
            grantType = root.GetSection("MailSettings:url").Value;
            //...
            _logger = logger;
        }

        public Task<TokenResponse> GetToken(TokenRequest request)
        {
            _logger.LogError(">>> GetToken......{RunTime}", DateTime.Now);

            throw new NotImplementedException();
        }
    }
}

