using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class ActividadEconomicaResponse
    {

        public int IdActividadEconomica { get; set; }

        public string? Descripcion { get; set; }

        public string? Grupo { get; set; }

        public int? EsIndustrial { get; set; }

        public string? NivelImpacto { get; set; }

        public int? Automatica { get; set; }

        public int? ValidarTurismo { get; set; }

        public string? Bombero { get; set; }

        public int? Activo { get; set; }

    }
}
