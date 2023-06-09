using Neptuno2022EF.Entidades.Dtos.CtaCte;
using Neptuno2022EF.Entidades.Dtos.Producto;
using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Ioc;
using Neptuno2022EF.Servicios.Interfaces;
using NuevaAppComercial2022.Entidades.Entidades;
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
    public partial class frmDetalleCtaCte : Form
    {
        private readonly IServiciosCtasCtes _servicio;
        private List<CtaCte> lista;
        private Cliente cliente;

        public void SetCtaCte(List<CtaCte> movimientos)
        {
            lista = movimientos;
        }
        public frmDetalleCtaCte()
        {
            InitializeComponent();
        }
        public frmDetalleCtaCte(IServiciosCtasCtes servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }
        private void dgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmDetalleCtaCte_Load(object sender, EventArgs e)
        {
            if (lista != null)
            {
                cliente = lista[0].Cliente;
                txtCliente.Text = cliente.Nombre.ToString();
                txtDireccion.Text = $"{cliente.Direccion} ";
                //txtLocalidad.Text = cliente.Ciudad..NombreLocalidad;
                //txtProvincia.Text = cliente.Domicilio.Provincia.NombreProvincia;

                if (lista.Count > 0)
                {
                    Helpers.FormHelper.MostrarDatosEnGrilla(dgDatos, lista);
                    var sumaHaber = 0m;
                    var sumaDebe = 0m;
                    foreach (var item in lista)
                    {
                        sumaDebe += item.Debe;
                        sumaHaber += item.Haber;
                    }
                    txtSaldo.Text = (sumaDebe - sumaHaber).ToString();
                }
            }

        }
        private void RecargarGrilla()
        {
            try
            {
                lista = _servicio.GetCtaCtePorClienteId(cliente.Id);
                //txtSaldo.Text = 
                if (lista.Count > 0)
                {
                    Helpers.FormHelper.MostrarDatosEnGrilla(dgDatos, lista);
                    var sumaHaber = 0m;
                    var sumaDebe = 0m;
                    foreach (var item in lista)
                    {
                        sumaDebe += item.Debe;
                        sumaHaber += item.Haber;
                    }
                    txtSaldo.Text = (sumaDebe - sumaHaber).ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnIngresarPago_Click(object sender, EventArgs e)
        {
            if ((dgDatos.SelectedRows.Count > 0) && (ValidarSaldoPendiente()))
            {
                frmPagoFactura frm = new frmPagoFactura(DI.Create<IServiciosCtasCtes>());
                frm.Text = "Pagar factura";
                DataGridViewRow r = dgDatos.CurrentRow;
                var movimiento = (CtaCte)r.Tag;

                frm.setMovimiento(movimiento);
                DialogResult dr = frm.ShowDialog(this);
                RecargarGrilla();
            }
            else
            {
                if (!ValidarSaldoPendiente())
                {
                    MessageBox.Show("Movimiento sin saldo negativo");
                }
            }
        }

        private bool ValidarSaldoPendiente()
        {
            DataGridViewRow r = dgDatos.CurrentRow;
            var movimiento = (CtaCte)r.Tag;
            return movimiento.Debe > 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
