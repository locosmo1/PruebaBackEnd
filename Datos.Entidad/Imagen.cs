using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class Imagen
    {
        public Imagen(){}

        public Imagen(int idImagen, int idProducto, string direccion)
        {
            this.idImagen = idImagen;
            this.idProducto = idProducto;
            this.direccion = direccion;
        }

        public int idImagen { get; set; }

        public int idProducto { get; set; }

        public string direccion { get; set; }

    }
}
