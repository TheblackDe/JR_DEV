using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CP_Entidades;

namespace CP_Datos
{
    public class CapaDatosHistorialVenta
    {
        // Método para obtener el historial de ventas con filtros opcionales de fecha y pedido
        public List<HistorialVenta> ObtenerHistorialVentas(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idPedido = null)
        {
            List<HistorialVenta> historialVentas = new List<HistorialVenta>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerHistorialVentas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (fechaInicio.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FechaInicio", DBNull.Value);
                        }

                        if (fechaFin.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FechaFin", DBNull.Value);
                        }

                        if (idPedido.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@IdPedido", idPedido.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IdPedido", DBNull.Value);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                historialVentas.Add(new HistorialVenta(
                                    Convert.ToDateTime(reader["FechaVenta"]),
                                    reader["Cliente"].ToString(),
                                    reader["Producto"].ToString(),
                                    Convert.ToDecimal(reader["Precio"]),
                                    Convert.ToInt32(reader["Cantidad"]),
                                    Convert.ToDecimal(reader["Total"]),
                                    Convert.ToInt32(reader["IDPedido"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }

            return historialVentas;
        }
    }
}
