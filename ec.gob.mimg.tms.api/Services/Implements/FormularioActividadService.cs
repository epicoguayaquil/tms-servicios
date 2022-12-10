using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioActividadService : CommonManager<TmsFormularioActividad>, IFormularioActividadService
    {
        public FormularioActividadService(TmsDbContext _dbContext) : base(new FormularioActividadRepository(_dbContext))
        {

        }

    }
}
