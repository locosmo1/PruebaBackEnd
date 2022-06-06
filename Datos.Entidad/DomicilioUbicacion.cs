using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class DomicilioUbicacion
    {
        public DomicilioUbicacion() { }
        public DomicilioUbicacion(int idDomicilio, String ciudad, String direccion, String latitud, String longitud)
        {
            this.idDomicilio = idDomicilio;
            this.ciudad = ciudad;
            this.direccion = direccion;
            this.latitud = latitud;
            this.longitud = longitud;
        }

        public int idDomicilio { get; set; }
        public String ciudad { get; set; }
        public String direccion { get; set; }
        public String latitud { get; set; }
        public String longitud { get; set; }


        public String tostring()
        {
            return "idDomicilio: " + idDomicilio + " ciudad: " + ciudad + " direccion: " + direccion;
        }
    }
}
