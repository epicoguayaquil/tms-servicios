﻿
namespace ec.gob.mimg.tms.api.DTOs.Request
{
    public class FormularioActividadRequest
    {
        public int IdActividadFormulario { get; set; }

        public int ActividadEconomicaId { get; set; }

        public int FormularioId { get; set; }

        public string? Observacion { get; set; }

    }
}
