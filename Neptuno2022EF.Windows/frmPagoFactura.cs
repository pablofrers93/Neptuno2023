using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neptuno2022EF.Windows
{
    public partial class frmPagoFactura : Form
    {
        private readonly IServiciosCtasCtes _servicio;
        private CtaCte ctaCte;
        public frmPagoFactura(IServiciosCtasCtes servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }
        private CtaCte _movimiento;

        internal void setMovimiento(CtaCte movimiento)
        {
            _movimiento = movimiento;
        }

        private void frmPagoFactura_Load(object sender, EventArgs e)
        {
            txtSaldo.Text = _movimiento.Debe.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ImporteText.Text) <= Convert.ToDecimal(txtSaldo.Text) && Convert.ToDecimal(ImporteText.Text)>0)
            {
                ctaCte = new CtaCte()
                {
                    CtaCteId = _movimiento.CtaCteId,
                    ClienteId = _movimiento.ClienteId,
                    NumeroFactura = _movimiento.NumeroFactura,
                    Haber = Convert.ToDecimal(ImporteText.Text),
                    Debe = _movimiento.Debe - Convert.ToDecimal(ImporteText.Text),
                    Saldo = _movimiento.Debe - Convert.ToDecimal(ImporteText.Text),
                    FechaMovimiento = DateTime.Now,
                    Movimiento = "pago"
                };
                _servicio.Guardar(ctaCte);
                DialogResult = DialogResult.OK;
            }
            else 
            if (Convert.ToDecimal(ImporteText.Text) > Convert.ToDecimal(txtSaldo.Text))
            {
                errorProvider1.SetError(ImporteText, "Importe no puede ser mayor");
            }
            else { errorProvider1.SetError(ImporteText, "Importe debe ser mayor a cero"); }
        }
    }
}
