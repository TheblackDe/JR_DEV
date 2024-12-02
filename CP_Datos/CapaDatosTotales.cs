using System;
using System.Data;
using System.Data.SqlClient;
using CP_Entidades;

namespace CP_Datos
{
    public class CapaDatosTotales
    {
        // Método para obtener los totales desde la base de datos
        public Totales ObtenerTotales()
        {
            Totales totales = new Totales();
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn)) // `Conn.cn` debe ser tu cadena de conexión
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerTotales", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Solo esperamos un registro con los totales
                            {
                                totales = new Totales
                                {
                                    TotalClientes = reader["TotalClientes"] != DBNull.Value ? Convert.ToInt32(reader["TotalClientes"]) : 0,
                                    TotalPedidos = reader["TotalPedidos"] != DBNull.Value ? Convert.ToInt32(reader["TotalPedidos"]) : 0,
                                    TotalArticulos = reader["TotalArticulos"] != DBNull.Value ? Convert.ToInt32(reader["TotalArticulos"]) : 0
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo del error (puedes loguear o lanzar la excepción)
                Console.WriteLine($"Error al obtener los totales: {ex.Message}");
            }

            return totales; // Devuelve la instancia de Totales, incluso si está vacía
        }
    }
}
