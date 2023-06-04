using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioObligacionService : CommonManager<TmsFormularioObligacion>, IFormularioObligacionService
    {
        public FormularioObligacionService(TmsDbContext _dbContext) : base(new FormularioObligacionRepository(_dbContext))
        {
        }

        public async Task<TmsFormularioObligacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdFormularioObligacion == id);
        }
        public async Task<ICollection<TmsFormularioObligacion>> GetListByFormularioId(int formularioId)
        {
            return await GetAsync(x => x.FormularioId == formularioId);
        }

        public async Task<TmsFormularioObligacion> GetByFormularioIdAndObligacionId(int formularioId, int obligacionId)
        {
            return await GetFirstOrDefaultAsync(x => x.FormularioId == formularioId && x.ObligacionId == obligacionId);
        }
    }
}
