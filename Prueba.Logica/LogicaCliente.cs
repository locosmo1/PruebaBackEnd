using Dapper;
using Prueba.Entidad;
using Prueba.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Prueba.Logica
{
    public class LogicaCliente
    {

        

        public void GuardarClienteCorreoYContraseña(Cliente cliente)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                int idCliente;
                try
                {
                    //consulta para averiguar cual es el numero mayor en id_Cliente
                    String sentencia = "SELECT MAX(idCliente) FROM Cliente";
                    idCliente = db.QueryFirstOrDefault<int>(sentencia);
                    idCliente = idCliente + 1;

                    //String sentencia11 = "SET IDENTITY_INSERT Cliente ON";
                    //var resultados = db.Execute(sentencia11);

                    String sentencia2 = "insert into Cliente(idCliente, nombreCompleto, apellidCompleto, fechaNacimiento, idUsuario) values (@idCliente, @nombreCompleto, @apellidCompleto, @fechaNacimiento, @idUsuario)";
                    var result = db.Execute(sentencia2, new { idCliente, cliente.nombreCompleto, cliente.apellidoCompleto, cliente.fechaNacimiento, LogicaSesion.idUsuario });

                    //String sentencia22 = "SET IDENTITY_INSERT Cliente OFF";
                    //var resultado = db.Execute(sentencia22);

                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }


        public void GuardarCliente(Cliente cliente)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                int idCliente;
                try
                {
                    //consulta para averiguar cual es el numero mayor en id_Cliente
                    String sentencia = "SELECT MAX(id_Cliente) FROM Cliente";
                    idCliente = db.QueryFirstOrDefault<int>(sentencia);
                    idCliente = idCliente + 1;

                    //String sentencia11 = "SET IDENTITY_INSERT Cliente ON";
                    //var resultados = db.Execute(sentencia11);

                    String sentencia2 = "insert into Cliente(idCliente, nombreCompleto, apellidoCompleto, fechaNacimiento, idUsuario) values (@idCliente, @nombreCompleto, @apellidoCompleto, @fechaNacimiento, @idUsuario)";
                    var result = db.Execute(sentencia2, new { idCliente, cliente.nombreCompleto, cliente.apellidoCompleto, cliente.fechaNacimiento, LogicaSesion.idUsuario });

                    //String sentencia22 = "SET IDENTITY_INSERT Cliente OFF";
                    //var resultado = db.Execute(sentencia22);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public Cliente ObtenerCliente(int id)
        {
            Cliente nuevo = null;
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    string sql = "select * from Cliente where idCliente=@idCliente";
                    nuevo = db.QueryFirstOrDefault<Cliente>(sql, new { id_Cliente = id });
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return nuevo;
        }

        public void ActualizarCliente(Cliente cliente)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "update Cliente set idCliente=@idCliente, nombreCompleto=@nombreCompleto, apellidoCompleto=@apellidoCompleto, fechaNacimiento=@fechaNacimiento where idUsuario=@id";
                    var result = db.Execute(cadena, new { cliente.idCliente, cliente.nombreCompleto, cliente.apellidoCompleto, cliente.fechaNacimiento, id = LogicaSesion.idUsuario });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void EliminarCliente(int id)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "delete from Carrito where idCarrito = @idCarrito";
                    var result = db.Execute(cadena, new { id });

                    string cadena2 = "select idCarrito from Carrito";
                    List<int> ids = (List<int>)db.Query<int>(cadena2);

                    //var resultado, encontrado;
                    foreach (int i in ids)
                    {
                        int resultado = i;
                        int encontrado = resultado;
                        if (resultado > id)
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
