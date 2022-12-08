using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class FormularioService : CommonManager<TmsFormulario>, IFormularioService
    {
        public FormularioService(TmsDbContext _dbContext) : base(new FormularioRepository(_dbContext))
        {

        }

    }
}
