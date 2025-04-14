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

        private void frmInicio_Load(object sender, EventArgs e)
        {
            clsConexionBD objConexionBD = new clsConexionBD();

            objConexionBD.ConectarBD();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsConexionBD objConexionBD = new clsConexionBD();

            DataTable contactos = objConexionBD.ObtenerContactos();

            dgvGrilla.DataSource = contactos;
            dgvGrilla.AutoGenerateColumns = true; // Asegura que no haya columnas manuales interfiriendo
            
        }
    }
}
