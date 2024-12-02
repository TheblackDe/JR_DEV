using System;
using System.Configuration;

namespace CP_Datos
{
    public class Conn
    {
        public static string cn;

        // Constructor estático que inicializa la cadena de conexión
        static Conn()
        {
            if (ConfigurationManager.ConnectionStrings["con_db"] != null)
            {
                cn = ConfigurationManager.ConnectionStrings["con_db"].ToString();
            }
            else
            {
                throw new ConfigurationErrorsException("La cadena de conexión 'con_db' no se encontró en el archivo de configuración.");
            }
        }
    }
}
