using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Datos
{
    public class Conexion
    {
        //private static String cadena = "Server=localhost\\SQLEXPRESS; Database=mercado; Trusted_Connection=True; MultipleActiveResultSets=True";
        //private static String cadena = "workstation id=mercadolibremeli.mssql.somee.com;packet size=4096;user id=locosmo1991_SQLLogin_1;pwd=5d6srbg5wv;data source=mercadolibremeli.mssql.somee.com;persist security info=False;initial catalog=mercadolibremeli";

        //private static String cadena = "workstation id=mercado.mssql.somee.com;packet size=4096;user id=locosmo1991_SQLLogin_1;pwd=5d6srbg5wv;data source=mercado.mssql.somee.com;persist security info=False;initial catalog=mercado";
        private static String cadena = "workstation id=domicilios.database.windows.net;packet size=1433;user id=domicilios;pwd=stiwar2266*;data source=domicilios.database.windows.net;persist security info=False;initial catalog=Domicilios";


        public static SqlConnection TraerConexionDB()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(cadena);

                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();

                return conexion;
            }
            catch (SqlException e)
            {
                throw new Exception("Fallo al abrir conexion: " + e.ToString());
            }
        }
    }
}
