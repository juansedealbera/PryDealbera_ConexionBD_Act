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
            DialogResult res = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                conexion.Eliminar(codSel);
                conexion.ListarBD(dgvGrilla);

                codSel = 0;
            }
        }

        private void numPrecio_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void numStock_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarCat_Click(object sender, EventArgs e)
        {
            string nuevaCategoria = txtAgregarCat.Text.Trim();

            if (string.IsNullOrEmpty(nuevaCategoria))
            {
                MessageBox.Show("Por favor ingrese un nombre para la categoría.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar la nueva categoría a la base de datos
            conexion.AgregarCategoria(nuevaCategoria);

            // Limpiar el TextBox
            txtAgregarCat.Clear();

            // Recargar las categorías en el ComboBox
            conexion.categorias(cmbCategorias);
        }

        private void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            string nombreBuscado = txtBuscarNombre.Text.Trim();

            if (string.IsNullOrEmpty(nombreBuscado))
            {
                MessageBox.Show("Por favor ingrese un nombre para realizar la búsqueda.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamar al método de búsqueda en la base de datos.
            conexion.BuscarPorNombre(nombreBuscado, dgvGrilla);
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            conexion.ListarBD(dgvGrilla);
        }

        private void dgvGrilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvGrilla.Rows[e.RowIndex];

                // Asegúrate que la columna se llama "Codigo" y tiene el valor correcto
                codSel = Convert.ToInt32(fila.Cells["Codigo"].Value);

                
                MessageBox.Show("Código seleccionado: " + codSel);
            }
        }
    }
}
