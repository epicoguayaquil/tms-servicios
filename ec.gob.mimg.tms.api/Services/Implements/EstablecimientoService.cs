using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using MessagePack.Formatters;
using ec.gob.mimg.tms.api.DTOs.Request;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class EstablecimientoService : CommonManager<TmsEstablecimiento>, IEstablecimientoService
    {
        public EstablecimientoService(TmsDbContext _dbContext) : base(new EstablecimientoRepository(_dbContext))
        {

        }

        public async Task<TmsEstablecimiento> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdEstablecimiento == id);
        }

        public async Task<TmsEstablecimiento> GetByEmpresaIdAndNumero(int empresaId, string numeroEstablecimiento)
        {
            return await GetFirstOrDefaultAsync(x => x.EmpresaId == empresaId && x.NumeroEstablecimiento == numeroEstablecimiento);
        }

        public async Task<bool> UpdateEstadoRegistroById(int id, string estadoRegistro)
        {
            TmsEstablecimiento establecimientoActual = await GetFirstOrDefaultAsync(x => x.IdEstablecimiento == id);
            if (establecimientoActual == null)
            {
                return false;
            }
            establecimientoActual.EstadoRegistro = estadoRegistro;
            establecimientoActual.FechaModificacion = DateTime.Now;
            establecimientoActual.UsuarioModificacion = "admin@mail.com";

            return await this.UpdateAsync(establecimientoActual);
        }
    }
}
