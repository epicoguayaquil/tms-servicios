﻿using ec.gob.mimg.tms.api.Data;
using ec.gob.mimg.tms.api.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class EstablecimientoService : CommonManager<TmsEstablecimiento>
    {
        public EstablecimientoService(TmsDbContext _dbContext) : base(new EstablecimientoRepository(_dbContext))
        {

        }

    }
}
