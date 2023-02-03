﻿using ec.gob.mimg.tms.srv.mail.Models;
using ec.gob.mimg.tms.srv.mimg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ec.gob.mimg.tms.srv.mimg.Services
{
    public interface IApiPatenteService
    {

        Task<DeclaracionResponse> GetByRuc(ContribuyenteApiRequest request);

    }
}
