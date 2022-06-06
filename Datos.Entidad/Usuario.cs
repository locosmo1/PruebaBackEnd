using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Entidad
{
    public class Usuario
    {
        public Usuario() {
            this.idUsuario = 0;
            this.usuario = "";
            this.contraseña = "";
            this.celular = "";
            this.idRol = 0;
        }

        public Usuario(int idUsuario, string usuario, string contraseña, String celular, int rol)
        {
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.contraseña = contraseña;
            this.celular = celular;
            this.idRol = rol;
        }

        public int idUsuario { get; set; }

        public String usuario { get; set; }

        public String contraseña { get; set; }

        public String celular { get; set; }

        public int idRol { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario &&
                   idUsuario == usuario.idUsuario &&
                   this.usuario == usuario.usuario &&
                   contraseña == usuario.contraseña &&
                   celular == usuario.celular &&
                   idRol == usuario.idRol;
        }

        public override int GetHashCode()
        {
            int hashCode = -538139412;
            hashCode = hashCode * -1521134295 + idUsuario.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(usuario);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(contraseña);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(celular);
            hashCode = hashCode * -1521134295 + idRol.GetHashCode();
            return hashCode;
        }
    }
}
