using ec.gob.mimg.tms.api.Models;
using EF.Core.Repository.Interface.Manager;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IEmpresaService : ICommonManager<TmsEmpresa>
    {
        Task<TmsEmpresa> GetByRucAsync(string ruc);
    }
}
