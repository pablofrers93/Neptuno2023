using Microsoft.Win32;
using Neptuno2022EF.Entidades.Dtos.Cliente;
using Neptuno2022EF.Entidades.Dtos.CtaCte;
using Neptuno2022EF.Entidades.Dtos.Producto;
using Neptuno2022EF.Entidades.Entidades;
using Neptuno2022EF.Ioc;
using Neptuno2022EF.Servicios.Interfaces;
using Neptuno2022EF.Windows.Helpers;
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
    public partial class frmCtasCtes : Form
    {
        private readonly IServiciosCtasCtes _servicio;
        private List<CtaCteListDto> lista;
        private List<CtaCteResumen> listaResumen;
        private int cantidadPorPagina = 20;
        private int registros;
        private int paginas;
        private int paginaActual = 1;
        public frmCtasCtes()
        {
            InitializeComponent();
        }
        public frmCtasCtes(IServiciosCtasCtes servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }

        private void tsbDetalles_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                frmDetalleCtaCte frm = new frmDetalleCtaCte(_servicio);
                frm.Text = "Detalle de Cta. Cte";
                DataGridViewRow r = dgvDatos.CurrentRow;
                var cta = (CtaCteResumen)r.Tag;
                List<CtaCte> listaMovimientosCtaCte = _servicio.GetCtaCtePorClienteId(cta.ClienteId);
                frm.SetCtaCte(listaMovimientosCtaCte);
                DialogResult dr = frm.ShowDialog(this);
                ActualizarGrilla();
            }
        }

        private void frmCtasCtes_Load(object sender, EventArgs e)
        {
            try
            {
                RecargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RecargarGrilla()
        {
            try
            {
                listaResumen = _servicio.GetCtaCtes_();
                MostrarDatosEnGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MostrarPaginado()
        {
            lista = _servicio.GetCtaCtesPorPagina(cantidadPorPagina, paginaActual);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            foreach (CtaCteResumen ctaCte in listaResumen)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, ctaCte);
                GridHelper.AgregarFila(dgvDatos, r);
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            Helpers.GridHelper.LimpiarGrilla(dgvDatos);
            RecargarGrilla();
        }
        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
