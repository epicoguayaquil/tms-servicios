using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioObligacionEjecucionService : CommonManager<TmsFormularioObligacionEjecucion>, IFormularioObligacionEjecucionService
    {
        public FormularioObligacionEjecucionService(TmsDbContext _dbContext) : base(new FormularioObligacionEjecucionRepository(_dbContext))
        {
        }

        public async Task<TmsFormularioObligacionEjecucion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdFormularioObligacionEjecucion == id);
        }
        public async Task<ICollection<TmsFormularioObligacionEjecucion>> GetListByFormularioObligacionId(int formularioObligacionId)
        {
            return await GetAsync(x => x.FormularioObligacionId == formularioObligacionId);
        }
    }
}
