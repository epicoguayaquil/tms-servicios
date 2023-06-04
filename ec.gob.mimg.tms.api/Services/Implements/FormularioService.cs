using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.Enums;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioService : CommonManager<TmsFormulario>, IFormularioService
    {
        public FormularioService(TmsDbContext _dbContext) : base(new FormularioRepository(_dbContext))
        {

        }

        public async Task<TmsFormulario> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdFormulario == id);
        }

        public async Task<ICollection<TmsFormulario>> GetListByEstablecimientoId(int establecimientoId)
        {
            return await GetAsync(x => x.EstablecimientoId == establecimientoId);
        }

        public async Task<ICollection<TmsFormulario>> GetListByEstablecimientoIdAndEstado(int establecimientoId, string estado)
        {
            return await GetAsync(x => x.EstablecimientoId == establecimientoId
                                    && x.Estado == estado);
        }

        public async Task<ICollection<TmsFormulario>> GetListByEstado(string estado)
        {
            return await GetAsync(x => x.Estado == estado);
        }
    }
}
