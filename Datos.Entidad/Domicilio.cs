using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class Domicilio
    {
        public Domicilio(){}
        public Domicilio(int idDomicilio, String ciudad, String direccion, int idUsuario, int idUbicacion)
        {
            this.idDomicilio = idDomicilio;
            this.ciudad = ciudad;
            this.direccion = direccion;
            this.idUsuario = idUsuario;
            this.idUbicacion = idUbicacion;
        }

        public int idDomicilio { get; set; }
        public String ciudad { get; set; }
        public String direccion { get; set; }
        public int idUsuario { get; set; }
        public int idUbicacion { get; set; }

        public String latitud { get; set; }

        public String longitud { get; set; }


        public String tostring()
        {
            return "idDomicilio: " + idDomicilio + " ciudad: " + ciudad + " direccion: " + direccion + " idUsuario: " + idUsuario + " idUbicacion: " + idUbicacion;
        }
    }
}
