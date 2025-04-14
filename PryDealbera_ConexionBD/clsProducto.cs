using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PryDealbera_ConexionBD
{
    public class clsProducto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int categoria { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }

        public clsProducto(int id, string codigo, string nombre, string descripcion, int categoria, decimal precio, int stock)
        {
            this.id = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.categoria = categoria;
            this.precio = precio;
            this.stock = stock;
        }
    }
}
