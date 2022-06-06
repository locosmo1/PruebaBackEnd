using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class Empresa
    {
        public Empresa() { }

        public Empresa(int idEmpresa, string nombre, string nit, int idUsuario)
        {
            this.idEmpresa = idEmpresa;
            this.nombre = nombre;
            this.nit = nit;
            this.idUsuario = idUsuario;
        }

        public int idEmpresa { get; set; }
        public String nombre { get; set; }
        public String nit { get; set; }
        public int idUsuario { get; set; }
    }
}
