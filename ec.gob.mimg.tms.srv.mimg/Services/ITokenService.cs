using System;
using ec.gob.mimg.tms.srv.mimg.DTOs;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(TokenRequest request);
    }
}

