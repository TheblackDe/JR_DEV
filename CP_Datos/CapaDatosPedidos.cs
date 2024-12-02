using System;
using System.Data;
using System.Data.SqlClient;
using CP_Entidades;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CP_Datos
{
    public class CapaDatosPedidos
    {
        // Método para insertar un nuevo pedido en la base de datos
        public int InsertarPedido(Pedido pedido, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    using (SqlCommand cmd = new SqlCommand("RegistrarPedido", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del pedido
                        cmd.Parameters.AddWithValue("@IdCliente", pedido.IdCliente);
                        cmd.Parameters.AddWithValue("@Comentario", pedido.Comentario ?? (object)DBNull.Value);

                        // Convertir los detalles del pedido a JSON
                        string articulosJson = ConvertirDetallesAPedidoJson(pedido.DetallePedidos);
                        cmd.Parameters.AddWithValue("@Articulos", articulosJson); // Artículos como JSON

                        // Parámetros de salida
                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        // Abrir conexión
                        conn.Open();

                        // Ejecutar el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        // Obtener los resultados de salida
                        resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                        mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = "Error al conectar a la base de datos: " + ex.Message;
                Console.WriteLine($"Error: {ex.Message}");
            }

            return resultado;
        }

        // Método privado para convertir los detalles del pedido a JSON
        private string ConvertirDetallesAPedidoJson(List<DetallePedido> detalles)
        {
            return JsonConvert.SerializeObject(detalles);
        }
    }
}
