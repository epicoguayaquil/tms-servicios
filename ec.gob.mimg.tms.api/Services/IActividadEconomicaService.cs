﻿using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IActividadEconomicaService : ICommonManager<TmsActividadEconomica>
    {
        ICollection<TmsActividadEconomica> GetByNivelAsync(int nivel);
    }
}