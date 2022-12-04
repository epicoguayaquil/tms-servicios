using ec.gob.mimg.tms.srv.sri.Models;
using EF.Core.Repository.Interface.Manager;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IEmpresaDataService : ICommonManager<EmpresaData>
    {
        Task<EmpresaData> GetByRucAsync(string ruc);
    }
}
