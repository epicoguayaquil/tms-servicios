using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioDetalleService : CommonManager<FormularioDetalle>, IFormularioDetalleService
    {
        public FormularioDetalleService(TmsDbContext _dbContext) : base(new FormularioDetalleRepository(_dbContext))
        {

        }

    }
}
