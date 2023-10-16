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
    public class LogicaDomicilio
    {

        public void GuardarDomiciliouUsuario(Domicilio domicilio)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                int idDomicilio, idUbicacion;
                //SELECT MAX(Id_Cliente) FROM Cliente;
                try
                {

                    //String sentencia3 = "SET IDENTITY_INSERT Domicilio ON";
                    //var resultados = db.Execute(sentencia3);

                    String sentencia2 = "SELECT MAX(idUbicacion) FROM Ubicacion";
                    idUbicacion = db.QueryFirstOrDefault<int>(sentencia2);
                    idUbicacion = idUbicacion + 1;

                    //Creacion del la ubicacion con latitud y longitud
                    String sentencia4 = "insert into Ubicacion(idUbicacion, latitud, longitud) values (@idUbicacion, @latitud, @longitud)";
                    var result2 = db.Execute(sentencia4, new { idUbicacion, domicilio.latitud, domicilio.longitud });

                    String sentencia3 = "SELECT MAX(idDomicilio) FROM Domicilio";
                    idDomicilio = db.QueryFirstOrDefault<int>(sentencia3);
                    idDomicilio = idDomicilio + 1;

                    String sentencia5 = "insert into Domicilio(idDomicilio, ciudad, direccion, idUsuario, idUbicacion) values (@idDomicilio, @ciudad, @direccion, @idUsuario, @id)";
                    var result3 = db.Execute(sentencia5, new { idDomicilio, domicilio.ciudad, domicilio.direccion, domicilio.idUsuario, id = idUbicacion });

                    //String sentencia5 = "SET IDENTITY_INSERT Domicilio OFF";
                    //var resultado = db.Execute(sentencia5);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public void GuardarDomicilio(Domicilio domicilio)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                int idDomicilio, idUbicacion;
                //SELECT MAX(Id_Cliente) FROM Cliente;
                try
                {

                    //String sentencia3 = "SET IDENTITY_INSERT Domicilio ON";
                    //var resultados = db.Execute(sentencia3);

                    String sentencia2 = "SELECT MAX(idUbicacion) FROM Ubicacion";
                    idUbicacion = db.QueryFirstOrDefault<int>(sentencia2);
                    idUbicacion = idUbicacion + 1;

                    //Creacion del la ubicacion con latitud y longitud
                    String sentencia4 = "insert into Ubicacion(idUbicacion, latitud, longitud) values (@idUbicacion, @latitud, @longitud)";
                    var result2 = db.Execute(sentencia4, new { idUbicacion, domicilio.latitud, domicilio.longitud });

                    String sentencia3 = "SELECT MAX(idDomicilio) FROM Domicilio";
                    idDomicilio = db.QueryFirstOrDefault<int>(sentencia3);
                    idDomicilio = idDomicilio + 1;

                    String sentencia5 = "insert into Domicilio(idDomicilio, ciudad, direccion, idUsuario, idUbicacion) values (@idDomicilio, @ciudad, @direccion, @idUsuario, @id)";
                    var result3 = db.Execute(sentencia5, new { idDomicilio, domicilio.ciudad, domicilio.direccion, LogicaSesion.idUsuario, id = idUbicacion });

                    //String sentencia5 = "SET IDENTITY_INSERT Domicilio OFF";
                    //var resultado = db.Execute(sentencia5);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public int ObtenerIdUbicacion(int idDomicilio)
        {
            int idUbicacion;
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string sql = "select idUbicacion from Domicilio where idDomicilio=@id";
                    idUbicacion = db.QueryFirstOrDefault<int>(sql, new { id = idDomicilio });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return idUbicacion;
        }

        public Domicilio ObtenerDomicilio(int idDomicilio)
        {
            Domicilio nuevo;
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string sql = "select * from Domicilio where idDomicilio=@idDomicilio";
                    nuevo = db.QueryFirstOrDefault<Domicilio>(sql, new { idDomicilio });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return nuevo;
        }

        public List<Domicilio> ObtenerDomicilioCliente(int idCliente)
        {
            List<Domicilio> domicilios;
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string sql = "select * from Domicilio where idCliente=@idCliente";
                    domicilios = (List<Domicilio>)db.Query<Domicilio>(sql, new { idCliente });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return domicilios;
        }

        public List<DomicilioUbicacion> ObtenerDomicilioUsuario(int idUsuario)
        {
            List<DomicilioUbicacion> domicilios;
            using (var db = Conexion.TraerConexionDB())
            {
                string sql = "select * from Domicilio where idUsuario = @id";
                domicilios = (List<DomicilioUbicacion>)db.Query<DomicilioUbicacion>(sql, new { id = idUsuario });
            }
            return domicilios;
        }

        public List<DomicilioUbicacion> ObtenerDomicilioUbicacionUsuario(int idUsuario)
        {
            List<DomicilioUbicacion> domicilios;
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string sql = "select idDomicilio, ciudad, direccion, latitud, longitud from Domicilio" +
                        " inner join Ubicacion on Domicilio.idUbicacion=Ubicacion.idUbicacion" +
                        " where idUsuario = @id";
                    domicilios = (List<DomicilioUbicacion>)db.Query<DomicilioUbicacion>(sql, new { id = idUsuario });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return domicilios;
        }

        public void ActualizarDomicilio(Domicilio domicilio)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    String sentencia3 = "SELECT MAX(idDomicilio) FROM Domicilio";
                    int idDomicilio = db.QueryFirstOrDefault<int>(sentencia3);

                    //Obtener la ubicacion relacionada con el domicilio
                    int idUsuario = LogicaSesion.usuarioActual.idUsuario;
                    String sentencia1 = "select idUbicacion from Ubicacion where idDomicilio=@id";
                    int idCliente = db.QueryFirstOrDefault<int>(sentencia1, new { id = idDomicilio });

                    string cadena = "update Domicilio set idDomicilio=@idDomicilio, ciudad=@ciudad, direccion=@direccion, idUsuario=@idUsuario, idUbicacion=@idUbicacion where id_Domicilio=@id";
                    var result = db.Execute(cadena, new { idDomicilio, domicilio.ciudad, domicilio.direccion, LogicaSesion.idUsuario, domicilio.idUbicacion, id = domicilio.idDomicilio });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void ActualizarDomicilioId(Domicilio Domicilio)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena1 = "update Ubicacion set latitud=@latitud, longitud=@longitud where idUbicacion=@ide";
                    var result1 = db.Execute(cadena1, new { Domicilio.latitud, Domicilio.longitud, ide = Domicilio.idUbicacion });

                    int id = Domicilio.idDomicilio;
                    string cadena = "update Domicilio set idDomicilio=@idDomicilio, ciudad=@ciudad, direccion=@direccion, idUsuario=@idUsuario, idUbicacion=@idUbicacion where idDomicilio=@ide";
                    var result = db.Execute(cadena, new { Domicilio.idDomicilio, Domicilio.ciudad, Domicilio.direccion, Domicilio.idUsuario, Domicilio.idUbicacion, ide = id });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void EliminarDomicilio(int id)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "delete from Domicilio where idDomicilio = @idDomicilio";
                    var result = db.Execute(cadena, new { idDomicilio = id });

                    string cadena2 = "select idDomicilio from Domicilio";
                    List<int> ids = (List<int>)db.Query<int>(cadena2);

                    //var resultado, encontrado;
                    foreach (int i in ids)
                    {
                        int resultado = i;
                        int encontrado = resultado;
                        if (resultado > id)
                        {
                            encontrado = encontrado - 1;
                            string consulta3 = "update Domicilio set idDomicilio=@encontrado where idDomicilio=@resultado";
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

        public void EliminarDomicilioUbicacion(Domicilio domicilio)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {

                    EliminarDomicilio(domicilio.idDomicilio);

                    EliminarUbicacion(domicilio.idUbicacion);

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void EliminarUbicacion(int idUbicacion)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    //Eliminar y modificar el id de la ubicacion
                    string cadena = "delete from Ubicacion where idUbicacion = @idUbicacion";
                    var result = db.Execute(cadena, new { idUbicacion });

                    string cadenaa = "select idUbicacion from Ubicacion";
                    List<int> idUbicaciones = (List<int>)db.Query<int>(cadenaa);
                    foreach (int i in idUbicaciones)
                    {
                        int resultado = i;
                        int encontrado = resultado;
                        if (resultado > idUbicacion)
                        {
                            encontrado = encontrado - 1;
                            string consulta4 = "update Ubicacion set idUbicacion=@idUbicacion where idUbicacion = @resultado";
                            var resultados = db.Execute(consulta4, new { encontrado, resultado = resultado });
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.ToString());
            }
        }

    }
}
