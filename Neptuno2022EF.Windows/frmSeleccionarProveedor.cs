using Neptuno2022EF.Entidades.Dtos.Proveedor;
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
    public partial class frmSeleccionarProveedor : Form
    {
        private ProveedorListDto proveedor;
        public frmSeleccionarProveedor()
        {
            InitializeComponent();
        }

        public ProveedorListDto GetProveedor()
        {
            return proveedor;
        }

        private void frmSeleccionarProveedor_Load(object sender, EventArgs e)
        {
            CombosHelper.CargarComboProveedores(ref cboProveedores);
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

            if (cboProveedores.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(cboProveedores, "Debe seleccionar un proveedor");
            }
            return valido;
        }

        private void cboProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProveedores.SelectedIndex > 0)
            {
                proveedor = (ProveedorListDto)cboProveedores.SelectedItem;
            }
            else
            {
                proveedor = null;
            }
        }
    }
}
