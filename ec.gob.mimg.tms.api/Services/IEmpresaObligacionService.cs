using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IEmpresaObligacionService : ICommonManager<TmsEmpresaObligacion>
    {
        Task<TmsEmpresaObligacion> GetById(int id);

        Task<ICollection<TmsEmpresaObligacion>> GetListByEmpresaId(int empresaId);

        Task<TmsEmpresaObligacion> GetByEmpresaIdAndObligacionId(int empresaId, int obligacionId);

    }
}
