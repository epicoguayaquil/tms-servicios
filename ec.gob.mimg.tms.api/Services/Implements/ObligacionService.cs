using ec.gob.mimg.tms.model.Models;
using ec.gob.mimg.tms.api.Repositories.Implements;
using EF.Core.Repository.Manager;
using ec.gob.mimg.tms.api.DTOs.Request;
using ec.gob.mimg.tms.api.DTOs.Response;
using ec.gob.mimg.tms.api.Enums;
using Microsoft.IdentityModel.Tokens;

namespace ec.gob.mimg.tms.api.Services.Implements
{
    public class ObligacionService : CommonManager<TmsObligacion>, IObligacionService
    {
        public ObligacionService(TmsDbContext _dbContext) : base(new ObligacionRepository(_dbContext))
        {
        }

        public async Task<TmsObligacion> GetById(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.IdObligacion == id);
        }

        public async Task<ICollection<TmsObligacion>> GetListByJerarquiaAndEstado(string jerarquia, string estado)
        {
            return await GetAsync(x => x.Jerarquia == jerarquia && x.Estado == estado);
        }

        public void ProcesarVariablesDeObligaciones(List<ObjetoObligacionResponse> objetoObligacionResponseList)
        {
            DeterminarNivelesPorDependencias(objetoObligacionResponseList);
            int nivelMayor = DeterminarNivelMayor(objetoObligacionResponseList);
            DeterminarSiSePuedenGestionarObligaciones(objetoObligacionResponseList, nivelMayor, 0);

        }

        private void DeterminarNivelesPorDependencias(List<ObjetoObligacionResponse> formularioObligacionResponseList)
        {
            foreach (var formularioObligacion in formularioObligacionResponseList)
            {
                if (formularioObligacion.Dependencias.IsNullOrEmpty())
                {
                    formularioObligacion.Nivel = 0;
                }
                else
                {
                    formularioObligacion.Nivel = -1;
                }
            }

            while (HayOBligacionesSinNiveles(formularioObligacionResponseList))
            {
                int cambiosRealizados = 0;
                foreach (var formularioObligacion in formularioObligacionResponseList)
                {
                    if (formularioObligacion.Nivel == -1)
                    {
                        List<ObjetoObligacionResponse> listaDePadres = BuscarPadres(
                            formularioObligacionResponseList, formularioObligacion.Dependencias);
                        if (!HayOBligacionesSinNiveles(listaDePadres))
                        {
                            cambiosRealizados++;
                            int nivelMayor = DeterminarNivelMayor(listaDePadres);
                            formularioObligacion.Nivel = nivelMayor + 1;
                        }
                    }
                }
                if (cambiosRealizados == 0)
                {
                    break;
                }
            }
        }

        private int DeterminarNivelMayor(List<ObjetoObligacionResponse> formularioObligacionResponseList)
        {
            int nivelMayor = -1;
            foreach (var formularioObligacion in formularioObligacionResponseList)
            {
                if (formularioObligacion.Nivel > nivelMayor)
                {
                    nivelMayor = formularioObligacion.Nivel;
                }
            }
            return nivelMayor;
        }

        private List<ObjetoObligacionResponse> BuscarPadres(List<ObjetoObligacionResponse> formularioObligacionResponseList,
            IEnumerable<ObligacionDependenciaResponse> padresList)
        {
            List<ObjetoObligacionResponse> nuevaListaDePadres = new();
            foreach (var dependencia in padresList)
            {
                foreach (var formularioObligacion in formularioObligacionResponseList)
                {
                    if (dependencia.ObligacionPadreId == formularioObligacion.ObligacionId)
                    {
                        nuevaListaDePadres.Add(formularioObligacion);
                        break;
                    }
                }
            }
            return nuevaListaDePadres;
        }

        private Boolean HayOBligacionesSinNiveles(List<ObjetoObligacionResponse> formularioObligacionResponseList)
        {
            if (formularioObligacionResponseList.IsNullOrEmpty())
            {
                return false;
            }
            foreach (var formularioObligacion in formularioObligacionResponseList)
            {
                if (formularioObligacion.Nivel == -1)
                {
                    return true;
                }
            }
            return false;
        }

        private void DeterminarSiSePuedenGestionarObligaciones(List<ObjetoObligacionResponse> formularioObligacionResponseList, int nivelMayor, int nivel)
        {
            if (formularioObligacionResponseList.IsNullOrEmpty())
            {
                return;
            }
            if (nivel > nivelMayor)
            {
                return;
            }
            MarcarGestionPorNivel(formularioObligacionResponseList, nivel, true);
            if (VerificarCumplimientoPorNivel(formularioObligacionResponseList, nivel))
            {
                DeterminarSiSePuedenGestionarObligaciones(formularioObligacionResponseList, nivelMayor, nivel + 1);
            }
            else
            {
                MarcarGestionDeNivelesSuperiores(formularioObligacionResponseList, nivel, false);
            }
        }

        private void MarcarGestionPorNivel(List<ObjetoObligacionResponse> formularioObligacionResponseList, int nivel, bool marcaGestion)
        {
            foreach (var formularioObligacion in formularioObligacionResponseList)
            {
                if (formularioObligacion.Nivel == nivel)
                {
                    formularioObligacion.SePuedeGestionar = marcaGestion;
                }
            }
        }

        private bool VerificarCumplimientoPorNivel(List<ObjetoObligacionResponse> formularioObligacionResponseList, int nivel)
        {
            if (formularioObligacionResponseList.IsNullOrEmpty())
            {
                return false;
            }
            foreach (var formularioObligacion in formularioObligacionResponseList)
            {
                if (formularioObligacion.Nivel == nivel)
                {
                    if (formularioObligacion.Estado == EstadoObligacionEnum.NO_CUMPLE.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void MarcarGestionDeNivelesSuperiores(List<ObjetoObligacionResponse> formularioObligacionResponseList, int nivel, bool marcaGestion)
        {
            foreach (var formularioObligacion in formularioObligacionResponseList)
            {
                if (formularioObligacion.Nivel > nivel)
                {
                    formularioObligacion.SePuedeGestionar = marcaGestion;
                }
            }
        }

    }
}
