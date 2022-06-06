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
    public class LogicaCarrito
    {
        public void CrearCarrito(Carrito carrito)
        {
            using (var dB = Conexion.TraerConexionDB())
            {
                int id_Carrito;
                try
                {
                    String sentencia = "SELECT MAX(idCarrito) FROM Carrito";
                    id_Carrito = dB.QueryFirstOrDefault<int>(sentencia);
                    id_Carrito = id_Carrito + 1;

                    //String sentencia1 = "SET IDENTITY_INSERT Carrito ON";
                    //var resultados = dB.Execute(sentencia1);

                    //Obtener la empresa relacionada con el usuario
                    int idUsuario = LogicaSesion.usuarioActual.idUsuario;
                    String sentencia1 = "select idCliente from Cliente where idUsuario=@id";
                    int idCliente = dB.QueryFirstOrDefault<int>(sentencia1, new { id = idUsuario });

                    String sentencia2 = "insert into Carrito(idCarrito, cantidad, idProducto, idCliente) values (@idCarrito, @cantidad, @idProducto, @idCliente)";
                    var result = dB.Execute(sentencia2, new { id_Carrito, carrito.cantidad, carrito.idProducto, idCliente });

                    //String sentencia3 = "SET IDENTITY_INSERT Carrito OFF";
                    //var resultado = dB.Execute(sentencia3);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public List<Carrito> ObtenerCarrito()
        {
            List<Carrito> carritos = new List<Carrito> { };
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    //Obtener la empresa relacionada con el usuario
                    int idUsuario = LogicaSesion.usuarioActual.idUsuario;
                    String sentencia1 = "select idCliente from Cliente where idUsuario=@id";
                    int idCliente = db.QueryFirstOrDefault<int>(sentencia1, new { id = idUsuario });

                    string cadena2 = "select * from Carrito where idCliente = @idCliente";
                    carritos = (List<Carrito>)db.Query<Carrito>(cadena2, new { idCliente });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return carritos;
        }

        public List<Carrito> ObtenerCarrito(int id_Cliente)
        {
            List<Carrito> carritos = new List<Carrito> { };
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    //Obtener la empresa relacionada con el usuario
                    int idUsuario = LogicaSesion.usuarioActual.idUsuario;
                    String sentencia1 = "select idCliente from Cliente where idUsuario=@id";
                    int idCliente = db.QueryFirstOrDefault<int>(sentencia1, new { id = idUsuario });

                    string cadena2 = "select * from Carrito where idCliente = @idCliente";
                    carritos = (List<Carrito>)db.Query<Carrito>(cadena2, new { idCliente });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return carritos;
        }

        public void ActualizarCarrito(Carrito carrito)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "update Carrito set idCarrito=@idCarrito, cantidad=@cantidad, idProducto=@idProducto, idCliente=@idCliente where idCarrito=@id";
                    var result = db.Execute(cadena, new { carrito.idCarrito, carrito.cantidad, carrito.idProducto, carrito.idCliente, id = carrito.idCarrito });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void EliminarCarrito(int idCarrito)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "delete from Carrito where idCarrito = @idCarrito";
                    var result = db.Execute(cadena, new { idCarrito });

                    string cadena2 = "select idCarrito from Carrito";
                    List<int> ids = (List<int>)db.Query<int>(cadena2);

                    //var resultado, encontrado;
                    foreach (int i in ids)
                    {
                        int resultado = i;
                        int encontrado = resultado;
                        if (resultado > idCarrito)
                        {
                            encontrado = encontrado - 1;
                            string consulta3 = "update Carrito set idCarrito=@encontrado where idCarrito=@resultado";
                            var resultados = db.Execute(consulta3, new { encontrado, resultado = resultado });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
