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
            conexion.categorias(cmbVerCategorias);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            decimal Precio = numPrecio.Value;
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
            numPrecio.Value,
            Convert.ToInt32(numStock.Value),
            Convert.ToInt32(cmbCategorias.SelectedValue)
            );

            conexion.Modificar(modificado);
            conexion.ListarBD(dgvGrilla);

            txtNombre.Clear();
            txtDescripcion.Clear();
            numPrecio.Value = 0;
            numStock.Value = 0;
            cmbCategorias.SelectedIndex = 0;

            codSel = 0;
        }

        private void btnEliminarCod_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada
            if (dgvGrilla.CurrentRow != null)
            {
                // Obtenemos el valor del código desde la fila seleccionada
                int codigoSeleccionado = Convert.ToInt32(dgvGrilla.CurrentRow.Cells["Codigo"].Value);

                // Confirmamos la eliminación
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Creamos una instancia de la clase de conexión
                    clsConexionBD conexion = new clsConexionBD();

                    // Llamamos al método Eliminar
                    conexion.Eliminar(codigoSeleccionado);

                    // Recargamos la grilla después de eliminar
                    conexion.ListarBD(dgvGrilla);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccioná una fila para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            string nombreBuscado = txtBuscarProducto.Text.Trim();

            if (string.IsNullOrEmpty(nombreBuscado))
            {
                MessageBox.Show("Por favor ingrese un nombre para realizar la búsqueda.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamar al método de búsqueda en la base de datos.
            conexion.BuscarPorNombre(nombreBuscado, dgvGrilla);

            txtBuscarProducto.Text = " ";
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            conexion.ListarBD(dgvGrilla);
        }

        private void btnVerCategorias_Click(object sender, EventArgs e)
        {
            if (cmbVerCategorias.SelectedValue != null)
            {
                int categoriaId = Convert.ToInt32(cmbVerCategorias.SelectedValue);
                conexion.BuscarPorCategoria(categoriaId, dgvGrilla);
            }
            else
            {
                MessageBox.Show("Por favor seleccioná una categoría válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void numPrecio_ValueChanged(object sender, EventArgs e)
        {
            numPrecio.Maximum = 500000000000;
        }

        private void numStock_ValueChanged(object sender, EventArgs e)
        {
            numStock.Maximum = 500000000000;
        }

        private void dgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
