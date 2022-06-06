using Dapper;
using Prueba.Datos;
using Prueba.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Logica
{
    public class LogicaImagenes
    {

        public static List<ImagenEntidad> listaImagenes = new List<ImagenEntidad>();

        public void RecibirImagen(ImagenEntidad imagen)
        {
            listaImagenes.Add(imagen);
        }

        public static void EnlazarImagenesProducto(int id_Producto)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                int idImagen;
                try
                {
                    foreach (ImagenEntidad imagen in listaImagenes)
                    {
                        String sentencia = "SELECT MAX(idImagen) FROM Imagen";
                        idImagen = db.QueryFirstOrDefault<int>(sentencia);
                        idImagen = idImagen + 1;

                        //String sentencia11 = "SET IDENTITY_INSERT Imagen ON";
                        //var resultados = db.Execute(sentencia11);

                        string cadena = "insert into Imagen(idImagen, idProducto, direccion) values " +
                            "(@idImagen, @idProducto, @direccion)";
                        var result = db.Execute(cadena, new { idImagen, id_Producto, imagen.direccion });

                        //String sentencia22 = "SET IDENTITY_INSERT Imagen OFF";
                        //var resultado = db.Execute(sentencia22);
                    }
                    listaImagenes.Clear();
                }
                catch (SqlException e)
                {
                    throw new Exception(e.ToString());
                }
            }
        }

        public List<string> ObtenerImagenes(int idProducto)
        {
            List<string> imagenes;
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    String sentencia = "select direccion from Imagen where idProducto = @id";
                    imagenes = (List<string>)db.Query<String>(sentencia, new { id = idProducto });
                }
                catch (SqlException e)
                {
                    throw new Exception(e.ToString());
                }
            }
            return imagenes;
        }

        public List<Imagen> ObtenerImagenesProducto(int idProducto)
        {
            List<Imagen> imagenes;
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    String sentencia = "select * from Imagen where idProducto = @id";
                    imagenes = (List<Imagen>)db.Query<Imagen>(sentencia, new { id = idProducto });
                }
                catch (SqlException e)
                {
                    throw new Exception(e.ToString());
                }
            }
            return imagenes;
        }

        public void ActualizarImagen(Imagen imagen, int id_Producto)
        {
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    foreach (ImagenEntidad imagenFor in listaImagenes)
                    {
                        string cadena = "update Imagen set idImagen=@idImagen, idProducto=@idProducto, direccion=@direccion where idProducto=@oldid";
                        var result = db.Execute(cadena, new { imagen.idImagen, id_Producto, imagen.direccion, oldid = id_Producto });
                    }
                }
                catch (SqlException e)
                {
                    throw new Exception(e.ToString());
                }
            }
        }

        public void EliminarImagen(int idImagen)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "delete from Imagen where id = @id";
                    var result = db.Execute(cadena, new { idImagen });

                    string cadena2 = "select idImagen from Imagen";
                    List<int> ids = (List<int>)db.Query<int>(cadena2);

                    //var resultado, encontrado;
                    foreach (int i in ids)
                    {
                        int resultado = i;
                        int encontrado = resultado;
                        if (resultado > idImagen)
                        {
                            encontrado = encontrado - 1;
                            string consulta3 = "update Imagen set idImagen=@encontrado where idImagen=@resultado";
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

        public void EliminarTodasLasImagenes(int id_Producto)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "delete from Imagen where idProducto = @idProducto";
                    var result = db.Execute(cadena, new { id_Producto });

                    string cadena2 = "select idImagen from Imagen";
                    List<int> ids = (List<int>)db.Query<int>(cadena2);
                    List<int> ids2 = (List<int>)db.Query<int>(cadena2);

                    ids.Sort();
                    int tamano = ids.Count;

                    //var resultado, encontrado;
                    for (int index = 0; index < tamano; index++)
                    {
                        string consulta3 = "update Imagen set idImagen=@encontrado where idImagen=@resultado";
                        var resultados = db.Execute(consulta3, new { encontrado = ids2[index], resultado = ids[index] });
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
