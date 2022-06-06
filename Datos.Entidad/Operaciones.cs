using System;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Entidad
{
    public class Operaciones
    {
        public List<SQL> grabar = new List<SQL>();
        private List<string> claves = new List<string>();
        private Dictionary<string, string> columnas = new Dictionary<string, string>();

        public Operaciones()
        {

        }

        public void Parametro(string clave, string valor, bool isSQL = false)
        {
            if (isSQL)
                columnas.Add(clave, valor);
            else
                columnas.Add(clave, "'" + valor + "'");

        }

        public void Parametro(string clave, object valor)
        {
            if (valor != null)
            {
                switch (valor.GetType().ToString())
                {
                    case "System.Boolean":
                        valor = "'" + valor + "'";
                        break;
                    case "System.Decimal":
                        valor = valor.ToString().Replace(",", ".");
                        break;
                    case "System.DateTime":
                        valor = "'" + ((DateTime)valor).ToString("yyyy-MM-ddTHH:mm:ss.fff") + "'";
                        break;
                    case "String":
                        valor = "'" + valor + "'";
                        break;
                }

                if (clave.Substring(0, 2).Equals("Id") && ((int)valor == -1 || (int)valor == 0))
                {
                    columnas.Add(clave, "@" + clave);
                    claves.Add(clave);
                }
                else
                {
                    columnas.Add(clave, valor + "");
                }
            }
        }

        public void Parametros(object lista)
        {
            object valor = null;
            foreach (var item in lista.GetType().GetProperties())
            {
                valor = item.GetValue(lista, null);
                switch (item.PropertyType.Name)
                {
                    case "Boolean":
                        valor = "'" + valor + "'";
                        break;
                    case "Decimal":
                        valor = valor.ToString().Replace(",", ".");
                        break;
                    case "DateTime":
                        valor = "'" + ((DateTime)valor).ToString("yyyy-MM-ddTHH:mm:ss.fff") + "'";
                        break;
                    case "String":
                        valor = "'" + valor + "'";
                        break;
                }

                if (item.Name.Substring(0, 2).Equals("Id") && item.PropertyType.Name.Equals("Int32") && ((int)valor == -1 || (int)valor == 0))
                {
                    columnas.Add(item.Name, "@" + item.Name);
                    claves.Add(item.Name);
                }
                else
                {
                    columnas.Add(item.Name, valor + "");
                }


            }
        }

        public void CrearInsertar(string tabla, int tipo = 0)
        {
            var split = tabla.Split('.');

            string esquema = "";

            if (split.Length > 1)
            {
                esquema = split[0] + ".";
                tabla = split[1];
            }

            string sql;
            if (tipo == 1 || tipo == 3)
            {
                sql = @"INSERT INTO {0} ({1}) OUTPUT INSERTED.{2} VALUES ({3})";
                sql = string.Format(sql, esquema + tabla, string.Join<string>(",", columnas.Keys.ToList()), "Id" + tabla, string.Join<string>(",", columnas.Values.ToList()));
            }
            else
            {
                sql = @"INSERT INTO {0} ({1}) VALUES ({2})";
                sql = string.Format(sql, esquema + tabla, string.Join<string>(",", columnas.Keys.ToList()), string.Join<string>(",", columnas.Values.ToList()));
            }

            grabar.Add(new SQL { Sql = sql, Tipo = tipo, Tabla = tabla, Claves = claves.ToList() });
            Limpiar();
        }

        public void CrearActualizar(string tabla, string where)
        {
            string campos = "";
            foreach (KeyValuePair<string, string> valor in columnas)
            {
                campos += valor.Key + "=" + valor.Value + ",";
            }
            campos = campos.Substring(0, campos.Length - 1);

            if (where.Length != 0)
            {
                where = " WHERE " + where;
            }
            string sql = "UPDATE " + tabla + " SET " + campos + where;

            grabar.Add(new SQL { Sql = sql });
            Limpiar();

        }

        public void CrearEliminar(string tabla, string where)
        {
            if (where.Length != 0)
            {
                where = " WHERE " + where;
            }

            string sql = "DELETE " + tabla + where;

            grabar.Add(new SQL { Sql = sql });
            Limpiar();

        }

        /// <param name="tipo">0:Ninguno 1:Padre 2: Hijo 3: HijoPadre </param>
        public void AgregarInsertar(string tabla, object lista, int tipo = 0)
        {
            List<string> columnas = new List<string>();
            List<string> valores = new List<string>();

            object valor = null;
            foreach (var item in lista.GetType().GetProperties())
            {
                columnas.Add(item.Name);
                valores.Add("@" + item.Name);

                valor = item.GetValue(lista, null);
                if (item.Name.Substring(0, 2).Equals("Id") && item.PropertyType.Name.Equals("Int32") && ((int)valor == -1 || (int)valor == 0))
                {
                    claves.Add(item.Name);
                }
            }

            var split = tabla.Split('.');

            string esquema = "";

            if (split.Length > 1)
            {
                esquema = split[0] + ".";
                tabla = split[1];
            }


            string sql;
            if (tipo == 1 || tipo == 3)
            {
                sql = @"INSERT INTO {0} ({1}) OUTPUT INSERTED.{2} VALUES ({3})";
                sql = string.Format(sql, esquema + tabla, string.Join<string>(",", columnas.ToList()), "Id" + tabla, string.Join<string>(",", valores.ToList()));
            }
            else
            {
                sql = @"INSERT INTO {0} ({1}) VALUES ({2})";
                sql = string.Format(sql, esquema + tabla, string.Join<string>(",", columnas.ToList()), string.Join<string>(",", valores.ToList()));
            }

            grabar.Add(new SQL { Sql = sql, Tipo = tipo, Tabla = tabla, Claves = claves.ToList(), Valores = lista });
            Limpiar();
        }

        public void AgregarActualizar(string tabla, object campos, string where)
        {
            List<string> columnas = new List<string>();
            var splitWhere = where.Split(',');

            foreach (var item in campos.GetType().GetProperties())
            {
                if (!splitWhere.Contains(item.Name))
                {
                    columnas.Add(item.Name + "=@" + item.Name);
                }
            }


            if (where.Length != 0)
            {
                where = " WHERE " + where + "=@" + where;
            }
            string sql = "UPDATE " + tabla + " SET " + string.Join<string>(",", columnas.ToList()) + where;

            grabar.Add(new SQL { Sql = sql, Valores = campos });
            Limpiar();

        }

        public void Limpiar()
        {
            columnas.Clear();
            claves.Clear();
        }



    }

    public class SQL
    {
        public string Sql { get; set; }
        public string Tabla { get; set; }
        public List<string> Claves { get; set; }
        public object Valores { get; set; }
        public int Tipo { get; set; } // 0:Ninguno 1:Padre 2: Hijo
    }
}