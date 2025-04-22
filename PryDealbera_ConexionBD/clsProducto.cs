using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PryDealbera_ConexionBD
{
    public class clsProducto
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public int categoria { get; set; }

        public clsProducto(int codigo, string nombre, string descripcion, decimal precio, int stock, int categoria)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.stock = stock;
            this.categoria = categoria;
        }
    }
}
