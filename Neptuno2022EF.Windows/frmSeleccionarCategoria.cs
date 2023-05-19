using Neptuno2022EF.Entidades.Entidades;
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
    public partial class frmSeleccionarCategoria : Form
    {
        public frmSeleccionarCategoria()
        {
            InitializeComponent();
        }
        private Categoria categoria;
        public Categoria GetCategoria()
        {
            return categoria;
        }

        private void frmSeleccionarCategoria_Load(object sender, EventArgs e)
        {
            CombosHelper.CargarComboCategorias(ref cboCategorias);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }
        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();

            if (cboCategorias.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboCategorias, "Debe seleccionar una categoria");
            }
            return valido;
        }

        private void cboCategorias_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboCategorias.SelectedIndex > 0)
            {
                categoria = (Categoria)cboCategorias.SelectedItem;
            }
            else
            {
                categoria = null;
            }

        }
    }
}
