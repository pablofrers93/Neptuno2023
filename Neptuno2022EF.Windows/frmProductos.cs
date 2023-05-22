using Microsoft.Win32;
using Neptuno2022EF.Entidades.Dtos.Ciudad;
using Neptuno2022EF.Entidades.Dtos.Producto;
using Neptuno2022EF.Entidades.Dtos.Proveedor;
using Neptuno2022EF.Entidades.Entidades;
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
    public partial class frmProductos : Form
    {
        private bool filtroOn = false;
        private int registros;
        private int paginas;
        private int paginaActual = 1;
        private int cantidadPorPagina = 5;
        public frmProductos(IServiciosProductos servicio)
        {
            InitializeComponent();
            _servicio = servicio;
        }
        private readonly IServiciosProductos _servicio;
        private List<ProductoListDto> lista;

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            FormHelper.MostrarDatosEnGrilla<ProductoListDto>(dgvDatos, lista);
            lblRegistros.Text = registros.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();
        }


        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmProductoAE frm = new frmProductoAE(_servicio) { Text = "Agregar Producto" };
            DialogResult dr = frm.ShowDialog(this);
            RecargarGrilla();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            try
            {
                var r = dgvDatos.SelectedRows[0];
                ProductoListDto productoDto = (ProductoListDto)r.Tag;
                DialogResult dr = MessageBox.Show($"¿Confirma la baja de {productoDto.NombreProducto}?",
                    "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }
                var producto = _servicio.GetProductoPorId(productoDto.ProductoId);
                if (producto == null)
                {
                    MessageBox.Show("Registro dado de baja por otro usuario", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    RecargarGrilla();
                    return;
                }
                if (!_servicio.EstaRelacionado(producto))
                {
                    _servicio.Borrar(producto.ProductoId);
                    GridHelper.BorrarFila(dgvDatos, r);
                    MessageBox.Show("Registro borrado satisfactoriamente!!!",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Registro relacionado...Baja denegada", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void RecargarGrilla()
        {
            try
            {
                if (filtroOn)
                {
                    registros = _servicio.GetCantidad(predicado);
                }
                else
                {
                    registros = _servicio.GetCantidad();

                }
                paginas = CalculosHelper.CalcularCantidadPaginas(registros, cantidadPorPagina);
                paginaActual = 1;
                MostrarPaginado();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MostrarPaginado()
        {
            if (filtroOn)
            {
                lista = _servicio.Filtrar(predicado, cantidadPorPagina, paginaActual);
            }
            else
            {
                lista = _servicio.GetProductosPorPagina(cantidadPorPagina, paginaActual);

            }
            MostrarDatosEnGrilla();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ProductoListDto productoDto = (ProductoListDto)r.Tag;
            var producto = _servicio.GetProductoPorId(productoDto.ProductoId);
            if (producto == null)
            {
                MessageBox.Show("Registro dado de baja por otro usuario");
                RecargarGrilla();
                return;

            }

            frmProductoAE frm = new frmProductoAE(_servicio) { Text = "Editar Producto" };
            frm.SetProducto(producto);
            DialogResult dr = frm.ShowDialog(this);
            RecargarGrilla();
        }

        Func<Producto, bool> predicado;
        private void tsbFiltrar_Click(object sender, EventArgs e)
        {
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();
            tsbFiltrar.BackColor = Color.White;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual == paginas)
            {
                return;
            }
            paginaActual++;
            MostrarPaginado();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual == 1)
            {
                return;
            }
            paginaActual--;
            MostrarPaginado();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = paginas;
            MostrarPaginado();
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            MostrarPaginado();
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSeleccionarCategoria frm = new frmSeleccionarCategoria() { Text = "Seleccionar..." };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                var categoriaSeleccionado = frm.GetCategoria();
                predicado = c => c.CategoriaId == categoriaSeleccionado.CategoriaId;
                filtroOn = true;
                RecargarGrilla();
                tsbFiltrar.BackColor = Color.Orange;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tsbFiltrar_ButtonClick(object sender, EventArgs e)
        {
            
        }

        private void proveedoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSeleccionarProveedor frm = new frmSeleccionarProveedor() { Text = "Seleccionar..." };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                var proveedorSeleccionado = frm.GetProveedor();
                predicado = c => c.ProveedorId == proveedorSeleccionado.ProveedorId;
                filtroOn = true;
                RecargarGrilla();
                tsbFiltrar.BackColor = Color.Orange;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void suspendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                predicado = c => c.Suspendido == true;
                filtroOn = true;
                RecargarGrilla();
                tsbFiltrar.BackColor = Color.Orange;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
