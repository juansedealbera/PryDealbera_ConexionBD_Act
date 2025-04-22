using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryDealbera_ConexionBD
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        //Instancias de clases//
        clsConexionBD conexion = new clsConexionBD();

        //Variable para guardar el codigo seleccionado//
        private int codSel = 0;

        private void frmInicio_Load(object sender, EventArgs e)
        {
            conexion.ConectarBD();
            conexion.ListarBD(dgvGrilla);
            conexion.categorias(cmbCategorias);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            decimal Precio = Convert.ToDecimal(numPrecio.Text);
            int Stock = Convert.ToInt32(numStock.Value);
            int CategoriaId = Convert.ToInt32(cmbCategorias.SelectedValue);


            clsProducto nuevoproducto = new clsProducto(0, Nombre, Descripcion, Precio, Stock, CategoriaId);

            conexion.Agregar(nuevoproducto);
            conexion.ListarBD(dgvGrilla);

            txtNombre.Clear();
            txtDescripcion.Clear();
            numPrecio.Value = 0;
            numStock.Value = 0;
            cmbCategorias.SelectedIndex = 0;

            btnModificar.Enabled = true;
            btnEliminarCod.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            clsProducto modificado = new clsProducto(
            codSel,
            txtNombre.Text,
            txtDescripcion.Text,
            Convert.ToDecimal(numPrecio.Text),
            Convert.ToInt32(numStock.Value),
            Convert.ToInt32(cmbCategorias.SelectedValue));

            conexion.Modificar(modificado);
            conexion.ListarBD(dgvGrilla);
        }

        private void btnEliminarCod_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEliminarCod.Text))
            {
                MessageBox.Show("Por favor ingrese el código del producto a eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int codigo;
            if (!int.TryParse(txtEliminarCod.Text, out codigo))
            {
                MessageBox.Show("Ingrese un código válido (número entero).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                conexion.EliminarPorCodigo(codigo);
                conexion.ListarBD(dgvGrilla);
                txtEliminarCod.Clear();
            }

        }
    }
}
