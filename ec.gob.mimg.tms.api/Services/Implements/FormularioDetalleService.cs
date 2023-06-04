using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioDetalleService : CommonManager<TmsFormularioDetalle>, IFormularioDetalleService
    {
        public FormularioDetalleService(TmsDbContext _dbContext) : base(new FormularioDetalleRepository(_dbContext))
        {
        }

        public async Task<TmsFormularioDetalle> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdFormularioDetalle == id);
        }
        public async Task<ICollection<TmsFormularioDetalle>> GetListByFormularioId(int formularioId)
        {
            return await GetAsync(x => x.FormularioId == formularioId);
        }

        public async Task<ICollection<TmsFormularioDetalle>> GetListByFormularioIdAndPasoCreacion(int formularioId, int pasoCreacion)
        {
            return await GetAsync(x => (x.FormularioId == formularioId && x.PasoCreacion == pasoCreacion));
        }

        public async Task<TmsFormularioDetalle> GetByFormularioIdAndCaracteristica(int formularioId, string caracteristica)
        {
            return await GetFirstOrDefaultAsync(x => (x.FormularioId == formularioId && x.Caracteristica == caracteristica));
        }
    }
}
