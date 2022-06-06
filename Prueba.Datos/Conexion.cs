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
        private static String cadena = "Server=localhost\\SQLEXPRESS; Database=mercado; Trusted_Connection=True; MultipleActiveResultSets=True";

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
