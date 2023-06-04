using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioObligacionCaracteristicaValorService : CommonManager<TmsFormularioObligacionCaracteristicaValor>, IFormularioObligacionCaracteristicaValorService
    {
        public FormularioObligacionCaracteristicaValorService(TmsDbContext _dbContext) : base(new FormularioObligacionCaracteristicaValorRepository(_dbContext))
        {
        }

        public async Task<TmsFormularioObligacionCaracteristicaValor> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdFormularioObligacionCaracteristicaValor == id);
        }

        public async Task<ICollection<TmsFormularioObligacionCaracteristicaValor>> GetListByFormularioObligacionId(int formularioObligacionId)
        {
            return await GetAsync(x => x.FormularioObligacionId == formularioObligacionId);
        }

    }
}
