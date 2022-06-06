//using Dapper;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Threading.Tasks;

//namespace Prueba.Datos
//{
//    public class ContextoDB
//    {

//        public static List<Dictionary<string, string>> TraerDiccionario(string sql)
//        {
//            List<Dictionary<string, string>> dic = new List<Dictionary<string, string>>();

//            try
//            {
//                using (var con = ContextoDB.TraerConexionDB())
//                using (SqlCommand comando = new SqlCommand(sql, con))
//                using (var dr = comando.ExecuteReader())
//                {
//                    while (dr.Read())
//                    {
//                        Dictionary<string, string> di = new Dictionary<string, string>();

//                        for (int i = 0; i < dr.FieldCount; i++)
//                        {
//                            di.Add(dr.GetName(i), dr.GetValue(i).ToString());
//                        }
//                        dic.Add(di);
//                    }
//                }

//            }
//            catch (Exception)
//            {
//                throw new ExceptionPropia("Fallo consulta a la base de datos");
//            }

//            return dic;
//        }

//        public static List<Dictionary<string, dynamic>> TraerDiccionarioDinamico(string sql)
//        {
//            List<Dictionary<string, dynamic>> dic = new List<Dictionary<string, dynamic>>();

//            try
//            {
//                using (var con = ContextoDB.TraerConexionDB())
//                using (SqlCommand comando = new SqlCommand(sql, con))
//                using (var dr = comando.ExecuteReader())
//                {
//                    while (dr.Read())
//                    {
//                        Dictionary<string, dynamic> di = new Dictionary<string, dynamic>();

//                        for (int i = 0; i < dr.FieldCount; i++)
//                        {
//                            di.Add(dr.GetName(i), dr.GetValue(i));
//                        }
//                        dic.Add(di);
//                    }
//                }

//            }
//            catch (Exception)
//            {
//                throw new ExceptionPropia("Fallo consulta a la base de datos");
//            }

//            return dic;
//        }

//        public static void Insertar(List<SQL> listaSQL)
//        {

//            Dictionary<string, int> datos = new Dictionary<string, int>();
//            string sql = "";
//            int pos = 0;
//            int dato = 0;
//            using (var db = TraerConexionDB())
//            {
//                using (var transaccion = db.BeginTransaction(IsolationLevel.ReadUncommitted))
//                {
//                    using (SqlCommand comando = new SqlCommand())
//                    {
//                        comando.Transaction = transaccion;
//                        comando.CommandTimeout = 600;
//                        comando.Connection = db;
//                        for (int i = 0; i < listaSQL.Count; i++)
//                        {
//                            var item = listaSQL[i];

//                            switch (item.Tipo)
//                            {
//                                case 0:
//                                    comando.CommandText = item.Sql;
//                                    comando.ExecuteNonQuery();
//                                    break;
//                                case 1:
//                                    comando.CommandText = item.Sql;
//                                    dato = int.Parse(comando.ExecuteScalar().ToString());

//                                    if (datos.ContainsKey("Id" + item.Tabla))
//                                        datos["Id" + item.Tabla] = dato;
//                                    else
//                                        datos.Add("Id" + item.Tabla, dato);

//                                    break;
//                                case 2:

//                                    sql = item.Sql;
//                                    foreach (var clave in item.Claves)
//                                    {
//                                        dato = datos[clave];
//                                        sql = sql.Replace("@" + clave, dato + "");

//                                    }
//                                    item.Sql = sql;

//                                    comando.CommandText = item.Sql;
//                                    comando.ExecuteNonQuery();
//                                    break;
//                                case 3:

//                                    sql = item.Sql;
//                                    foreach (var clave in item.Claves)
//                                    {
//                                        dato = datos[clave];
//                                        sql = sql.Replace("@" + clave, dato + "");

//                                    }
//                                    item.Sql = sql;

//                                    comando.CommandText = item.Sql;

//                                    dato = int.Parse(comando.ExecuteScalar().ToString());

//                                    if (datos.ContainsKey("Id" + item.Tabla))
//                                        datos["Id" + item.Tabla] = dato;
//                                    else
//                                        datos.Add("Id" + item.Tabla, dato);

//                                    break;
//                            }

//                            pos = i;

//                        }

//                        transaccion.Commit();
//                    }
//                }
//            }

//        }

//        public static void InsertarNuevo(List<SQL> listaSQL)
//        {

//            Dictionary<string, int> datos = new Dictionary<string, int>();
//            string sql = "";
//            int pos = 0;
//            int dato = 0;
//            using (var db = TraerConexionDB())
//            {
//                using (var transaccion = db.BeginTransaction(IsolationLevel.ReadUncommitted))
//                {

//                    for (int i = 0; i < listaSQL.Count; i++)
//                    {
//                        var item = listaSQL[i];

//                        switch (item.Tipo)
//                        {
//                            case 0:
//                                db.Execute(item.Sql, item.Valores, transaccion);
//                                break;
//                            case 1:
//                                dato = db.ExecuteScalar<int>(item.Sql, item.Valores, transaccion);

//                                if (datos.ContainsKey("Id" + item.Tabla))
//                                    datos["Id" + item.Tabla] = dato;
//                                else
//                                    datos.Add("Id" + item.Tabla, dato);
//                                break;
//                            case 2:

//                                sql = item.Sql;
//                                foreach (var clave in item.Claves)
//                                {
//                                    dato = datos[clave];
//                                    //item.Valores = dato;
//                                }
//                                db.Execute(item.Sql, item.Valores, transaccion);
//                                break;
//                            case 3:
//                                sql = item.Sql;
//                                foreach (var clave in item.Claves)
//                                {
//                                    dato = datos[clave];
//                                    //item.Valores = dato;
//                                }

//                                dato = db.ExecuteScalar<int>(item.Sql, item.Valores, transaccion);

//                                if (datos.ContainsKey("Id" + item.Tabla))
//                                    datos["Id" + item.Tabla] = dato;
//                                else
//                                    datos.Add("Id" + item.Tabla, dato);

//                                break;
//                        }

//                        pos = i;

//                    }

//                    transaccion.Commit();
//                }
//            }

//        }


//        public async static Task<SqlConnection> GetOpenConnection()
//        {
//            string cadena = @"Data Source=.\SQLEXPRESS; Initial Catalog=EPSAS; uid=innovasoft; password=ingeniero91@";
//            var connection = new SqlConnection(cadena);
//            await connection.OpenAsync();
//            return connection;
//        }

//        public static void Insertar(string sql)
//        {
//            using (var db = TraerConexionDB())
//            {
//                Dapper.SqlMapper.Execute(db, sql);
//            }

//        }

//        public static SqlConnection TraerConexionDB()
//        {
//            try
//            {

//                //string cadena = @"Data Source=.\localhost\SQLEXPRESS; Initial Catalog=EPSAS; uid=innovasoft; password=";
//                //string cadena = ("server=DESKTOP-774BQEK\\SQLEXPRESS ; database=DataBasePruebas ; integrated security = true");
//                string cadena = "Server=localhost\\SQLEXPRESS; Database=master; Trusted_Connection=True";

//                SqlConnection conexion;
//                conexion = new SqlConnection(cadena);

//                if (conexion.State == ConnectionState.Closed)
//                    conexion.Open();

//                return conexion;

//            }
//            catch
//            {
//                throw new ExceptionPropia("Fallo al abrir conexion");
//            }
//        }
//    }
//}