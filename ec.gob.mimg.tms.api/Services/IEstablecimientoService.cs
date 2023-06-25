using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IEstablecimientoService : ICommonManager<TmsEstablecimiento>
    {
        Task<TmsEstablecimiento> GetById(int id);

        Task<TmsEstablecimiento> GetByEmpresaIdAndNumero(int empresaId, string numeroEstablecimiento);

        Task<bool> UpdateEstadoRegistroById(int id, string estadoRegistro);
    }
}
