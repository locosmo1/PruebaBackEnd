using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class ImagenEntidad
    {

        public ImagenEntidad(){}

        public ImagenEntidad(string direccion)
        {
            this.direccion = direccion;
        }
        public string direccion { get; set; }
    }
}
