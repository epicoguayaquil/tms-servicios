﻿using ec.gob.mimg.tms.model.Models;
using EF.Core.Repository.Repository;

namespace ec.gob.mimg.tms.api.Repositories.Implements
{
    public class EstablecimientoRepository : CommonRepository<TmsEstablecimiento>, IEstablecimientoRepository
    {
        public EstablecimientoRepository(TmsDbContext dbContext) : base(dbContext) {
        
        }

      
    }
}
