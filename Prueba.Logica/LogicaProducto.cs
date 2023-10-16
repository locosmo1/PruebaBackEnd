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
    public class LogicaProducto
    {

        public static int idProducto;

        public void CrearProducto(Producto producto)
        {
            try
            {
                idProducto = 0;
                using (var db = Conexion.TraerConexionDB())
                {
                    String sentencia = "SELECT MAX(idProducto) FROM Producto";
                    idProducto = db.QueryFirstOrDefault<int>(sentencia);
                    idProducto = idProducto + 1;

                    //Obtener la empresa relacionada con el usuario
                    int idEmpresa = 0;
                    if (producto.idEmpresa==0)
                    {
                        int idUsuario = LogicaSesion.usuarioActual.idUsuario;
                        String sentencia1 = "select idEmpresa from Empresa where idUsuario=@id";
                        idEmpresa = db.QueryFirstOrDefault<int>(sentencia1, new { id = idUsuario });
                    }
                    else
                    {
                        int idUsuario = producto.idEmpresa;
                        String sentencia1 = "select idEmpresa from Empresa where idUsuario=@id";
                        idEmpresa = db.QueryFirstOrDefault<int>(sentencia1, new { id = idUsuario });
                    }

                    //String sentencia11 = "SET IDENTITY_INSERT Producto ON";
                    //var resultados = db.Execute(sentencia11);

                    string cadena = "insert into producto(idProducto, imagen, titulo, precio, color, cantidad, descripcion, idEmpresa, idCategoria) values " +
                        "(@idProducto, @imagen, @titulo, @precio, @color, @cantidad, @descripcion, @idEmpresa, @idCategoria)";
                    var result = db.Execute(cadena, new
                    {
                        idProducto,
                        producto.imagen,
                        producto.titulo,
                        producto.precio,
                        producto.color,
                        producto.cantidad,
                        producto.descripcion,
                        idEmpresa,
                        producto.idCategoria
                    });

                    //String sentencia22 = "SET IDENTITY_INSERT Producto OFF";
                    //var resultado = db.Execute(sentencia22);

                    //Enlazar las imagenes al pruducto
                    LogicaImagenes.EnlazarImagenesProducto(idProducto);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }



        public List<Producto> ObtenerProductosCarrito()
        {
            List<int> idProductos;
            List<int> unidades;
            List<Producto> productos = new List<Producto>() { };

            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    //Obtener el cliente relacionada con el usuario
                    int idUsuario = LogicaSesion.usuarioActual.idUsuario;
                    String sentencia1 = "select idCliente from Cliente where idUsuario=@id";
                    int idCliente = db.QueryFirstOrDefault<int>(sentencia1, new { id = idUsuario });

                    string cadena = "select idProducto from Carrito where idCliente = @id";
                    idProductos = (List<int>)db.Query<int>(cadena, new { id = idCliente });

                    string cadena2 = "select cantidad from Carrito where idCliente = @id";
                    unidades = (List<int>)db.Query<int>(cadena2, new { id = idCliente });

                    foreach (int element in idProductos)
                    {
                        string cadena3 = "select * from Producto where idProducto = @element";
                        productos.Add(db.QueryFirstOrDefault<Producto>(cadena3, new { element }));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return productos;
        }

        public List<Producto> ListarDapper()
        {
            List<Producto> productos;
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "select * from Producto"; //id, imagen, titulo, precio, descripcion, cantidad
                    productos = (List<Producto>)db.Query<Producto>(cadena);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.ToString());
            }
            return productos;
        }


        public Producto ObtenerProducto(int ide)
        {
            Producto producto = null;
            using (var db = Conexion.TraerConexionDB())
            {
                try
                {
                    String sentencia = "select * from Producto where idProducto = @id";
                    producto = (Producto)db.QueryFirstOrDefault<Producto>(sentencia, new { id = ide });
                }
                catch (SqlException e)
                {
                    throw new Exception(e.ToString());
                }
            }
            return producto;
        }

        /// <summary>
        /// Obtener los productos de una empresa en especifico
        /// </summary>
        /// <param name="ide"></param>
        /// <returns></returns>
        public List<Producto> ObtenerProductoEmpresa(int idUsuario)
        {//idUsuario
            List<Producto> productos;
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena2 = "select idEmpresa from Empresa where idUsuario = @id";
                    int idEmpresa = db.QueryFirstOrDefault<int>(cadena2, new { id = idUsuario});
                    string cadena = "select * from Producto where idEmpresa = @id"; //id, imagen, titulo, precio, descripcion, cantidad
                    productos = (List<Producto>)db.Query<Producto>(cadena, new { id = idEmpresa });
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.ToString());
            }
            return productos;
        }


        public void EditarProducto(Producto producto)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "update Producto set idProducto=@idProducto, imagen=@imagen, titulo=@titulo, precio=@precio, color=@color, cantidad=@cantidad, descripcion=@descripcion, idEmpresa=@idEmpresa, idCategoria=@idCategoria where idProducto=@oldid";
                    var result = db.Execute(cadena, new { producto.idProducto, producto.imagen, producto.titulo, producto.precio, producto.color, producto.cantidad, producto.descripcion, producto.idEmpresa, producto.idCategoria, oldid = producto.idProducto });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public void EliminarProducto(int idProducto)
        {
            try
            {
                using (var db = Conexion.TraerConexionDB())
                {
                    string cadena = "delete from Producto where idProducto = @idProducto";
                    var result = db.Execute(cadena, new { idProducto });

                    string cadena2 = "select idProducto from Producto";
                    List<int> ids = (List<int>)db.Query<int>(cadena2);

                    //var resultado, encontrado;
                    foreach (int i in ids)
                    {
                        int resultado = i;
                        int encontrado = resultado;
                        if (resultado > idProducto)
                        {
                            encontrado = encontrado - 1;
                            string consulta3 = "update Producto set idProducto=@encontrado where idProducto=@resultado";
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

        public Boolean ElCampoEsCorrecto(string campo)
        {
            bool respuesta = false;
            List<string> prohibidos = ObtenerListadoPalabrasProhibidas();
            foreach (string elemento in prohibidos)
            {
                if (campo.Equals(elemento) || campo.SequenceEqual(elemento))
                {
                    respuesta = false;
                }
                else
                {
                    respuesta = true;
                }
            }
            
            for (int i=0; i<campo.Length; i++)
            {
                foreach (string elemento in prohibidos)
                {
                    if (campo.Substring(i, 1).Equals(elemento))
                    {
                        respuesta = false;
                    }
                }
            }
            
            return respuesta;
        }

        public List<string> ObtenerListadoPalabrasProhibidas()
        {
            string[] input = { "(", ")", "{", "}", "[", "]", "/", "|", "'\'", "<", ">", "==", ">=", "<=", "!=",
                "if", "for", "foreach", "switch", "<script>", "</script>",
                "script", "abstract","as","base", "break", "case", "catch", "checked", "class", "const", "continue",
                "default", "decimal", "delagate", "do", "else", "enum", "event", "explicit", "extern", "event", "explicit", "extern", "false", "finally", "fixed", "goto", "implicit",
                "in", "interface", "internal", "is", "lock", "long", "namespace", "new", "null","operator",
                "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sealed", "short", "sizeof", "stackalloc", "static", "struct", "this", "typeof", "throw",
                "try", "uint", "ulong", "unchecked", "unsafe", "using", "virtual", "void", "volatile", "while", "add", "and", "alias", "ascending", "args", "async", "await", "by",
                "descending", "dinamic", "equals", "from", "get", "global", "group", "init", "into", "join", "let", "maneged", "nameof", "nint", "not", "notnull", "nuint", "on",
                "or", "orderby", "partial", "record", "remove", "select", "set", "unmanaged", "value", "var", "when", "where", "whit", "yield",
                "List<byte>", "List<sbyte>", "List<int>", "List<uint>", "List<short>", "List<ushort>", "List<long>", "List<float>", "List<Double>", "List<char>",
                "List<bool>", "List<object>", "List<decimal>", "List<uint>", "List<short>", "List<ushort>", "List<long>", "List<float>", "List<Double>", "List<char>",
                "List<int>", "List<string>", "List<String>", "List<boolean>", "List<Boolean>",
                "List<List<int>>", "List<List<string>>", "List<List<String>>", "List<List<boolean>>", "List<List<Boolean>>",
                "byte", "sbyte", "int", "uint", "short", "ushort", "long", "float", "Double", "char", "bool", "object", "string", "String", "decimal", "Boolean", "boolean" };
            List<string> caracteresProhibidos = new List<string>(input);
            HashSet<string> hashWithoutDuplicates = new HashSet<string>(caracteresProhibidos);
            List<string> listWithoutDuplicates = hashWithoutDuplicates.ToList();
            return listWithoutDuplicates;
        }


    }
}
