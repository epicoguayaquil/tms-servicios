using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Interface.Manager;

namespace ec.gob.mimg.tms.api.Services
{
    public interface IObligacionService : ICommonManager<TmsObligacion>
    {
        Task<TmsObligacion> GetById(int id);

        Task<ICollection<TmsObligacion>> GetListByJerarquiaAndEstado(string jerarquia, string estado);

        void ProcesarVariablesDeObligaciones(List<ObjetoObligacionResponse> objetoObligacionResponseList);
    }
}
