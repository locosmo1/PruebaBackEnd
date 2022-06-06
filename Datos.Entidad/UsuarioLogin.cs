using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class UsuarioLogin{

        public UsuarioLogin(){}

        public UsuarioLogin(string usuario, string contraseña)
        {
            this.usuario = usuario;
            this.contraseña = contraseña;
        }

        public String usuario { get; set; }

        public String contraseña { get; set; }

    }

    

    
}
