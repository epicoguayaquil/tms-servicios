﻿using System;
using ec.gob.mimg.tms.srv.mimg.DTOs;

namespace ec.gob.mimg.tms.srv.mimg.Services.Implements
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(TokenRequest request);
        
        Task<TokenResponse> GetTokenTasa(TokenRequest request);

        Task<TokenResponse> GetTokenHabilitacion(TokenRequest request);

        Task<TokenResponse> GetTokenActivoMil(TokenRequest request);

        Task<TokenResponse> GetTokenPatente(TokenRequest request);
    }
}

