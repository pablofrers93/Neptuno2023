using Neptuno2022EF.Entidades.Dtos.CtaCte;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Servicios.Interfaces
{
    public interface IServiciosCtasCtes
    {
        List<CtaCte> GetCtaCtes();
        List<CtaCteResumen> GetCtaCtes_();
        void Guardar(CtaCte ctaCte);
        void Borrar(int id);
        bool Existe(CtaCte ctaCte);
        CtaCte GetCtaCtePorId(int ctaCteId);
        List<CtaCte> GetCtaCtePorClienteId(int ctaCteId);
        bool EstaRelacionado(CtaCte ctaCte);
        List<CtaCteListDto> GetCtaCtesPorPagina(int cantidad, int pagina);
        int GetCantidad();
    }
}
