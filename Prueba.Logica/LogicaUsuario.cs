using Dapper;
using Prueba.Datos;
using Prueba.Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Logica
{
    public class LogicaUsuario
    {

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    String sentencia = "select * from Usuario";
                    usuarios = (List<Usuario>)db.Query<Usuario>(sentencia);
                }
                catch (SqlException e)
                {
                    throw new Exception(e.ToString());
                }
            }
            return usuarios;
        }

        public void GuardarUsuario(Usuario usuario)
        {
            String contraseña;
            //Primero crear el usuario con usuario(Correo electronico), contraseña, celular, rol
            using (var db = Conexion.TraerConexionDB())
            {
                int idUsuario;
                usuario.idRol = 1;
                contraseña = Cryptography.EncriptarContraseña(usuario.contraseña);
                try
                {
                    String sentencia = "SELECT MAX(idUsuario) FROM Usuario";
                    idUsuario = db.QueryFirstOrDefault<int>(sentencia);
                    idUsuario = idUsuario + 1;

                    String sentencia2 = "insert into Usuario(idUsuario, usuario, contraseña, celular, idRol) values (@idUsuario, @usuario, @contraseña, @celular, @idRol)";
                    var result = db.Execute(sentencia2, new { idUsuario, usuario.usuario, contraseña, usuario.celular, usuario.idRol });
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

    }
}
