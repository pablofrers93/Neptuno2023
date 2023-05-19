using Neptuno2022EF.Entidades.Dtos.CtaCte;
using Neptuno2022EF.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Datos.Interfaces
{
    public interface IRepositorioCtasCtes
    {
        List<CtaCte> GetCtasCtes();
        List<CtaCteResumen> GetCtasCtes_();
        void Agregar(CtaCte ctaCte);
        void Editar(CtaCte ctaCte);
        void Borrar(int id);
        bool Existe(CtaCte ctaCte);
        CtaCte GetCtaCtePorId(int ctaCteId);
        List<CtaCte> GetCtaCtePorClienteId(int ctaCteId);
        bool EstaRelacionado(CtaCte ctaCte);
        List<CtaCteListDto> GetCtasCtesPorPagina(int cantidad, int pagina);
        int GetCantidad();
    }
}
