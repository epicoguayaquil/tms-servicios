using System.ComponentModel.DataAnnotations;
using ec.gob.mimg.tms.model.Models;

namespace ec.gob.mimg.tms.api.DTOs.Response
{
    public class ActividadEconomicaResponse
    {

        public int IdActividadEconomica { get; set; }

        public string? Codigo { get; set; }

        public string? Descripcion { get; set; }

        public int? Nivel { get; set; }

        public string? Estado { get; set; }

    }
}
