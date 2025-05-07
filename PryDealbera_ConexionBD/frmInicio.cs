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
            if (dgvGrilla.SelectedRows.Count > 0)
            {
                clsProducto producto = new clsProducto(
                    Convert.ToInt32(dgvGrilla.SelectedRows[0].Cells["Codigo"].Value),
                    txtNombre.Text,
                    txtDescripcion.Text,
                    decimal.Parse(numPrecio.Text),
                    int.Parse(numStock.Text),
                    Convert.ToInt32(cmbCategorias.SelectedValue)
                );

                clsConexionBD objConexion = new clsConexionBD();
                objConexion.Modificar(producto);

                objConexion.ListarBD(dgvGrilla);
            }
            else
            {
                MessageBox.Show("Seleccioná un producto para modificar.");
            }
        }

        private void btnEliminarCod_Click(object sender, EventArgs e)
        {
            if (dgvGrilla.CurrentRow != null)
            {
                //valor del código desde la fila seleccionada
                int codigoSeleccionado = Convert.ToInt32(dgvGrilla.CurrentRow.Cells["Codigo"].Value);

                DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    clsConexionBD conexion = new clsConexionBD();

                    conexion.Eliminar(codigoSeleccionado);

                    conexion.ListarBD(dgvGrilla);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccioná una fila para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAgregarCat_Click(object sender, EventArgs e)
        {
            string nuevaCategoria = txtAgregarCat.Text.Trim();

            if (string.IsNullOrEmpty(nuevaCategoria))
            {
                MessageBox.Show("Por favor ingrese un nombre para la categoría.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            conexion.AgregarCategoria(nuevaCategoria);

            txtAgregarCat.Clear();

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

        private void dgvGrilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvGrilla.Rows[e.RowIndex];

                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
                numPrecio.Text = fila.Cells["Precio"].Value.ToString();
                numStock.Text = fila.Cells["Stock"].Value.ToString();
                cmbCategorias.SelectedValue = fila.Cells["CategoriaId"].Value;

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Deseas Salir?", "Confirma");
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
