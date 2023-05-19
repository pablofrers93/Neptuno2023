using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuno2022EF.Entidades.Dtos.CtaCte
{
    public class CtaCteResumen
    {
        public string Cliente { get; set; }
        public decimal Saldo { get; set; }
        public int ClienteId { get; set; }
        public string NumeroFactura { get; set; }
    }
}
