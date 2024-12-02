using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CP_Entidades;

namespace CP_Datos
{
    public class CapaDatosArticulos
    {
        // Listar todos los artículos
        public List<Articulo> Listar()
        {
            List<Articulo> Lista = new List<Articulo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    conn.Open();
                    string query = "SELECT Id, Nombre, Descripcion, Precio, Stock FROM Articulos";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lista.Add(new Articulo()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }

            return Lista;
        }

        // Registrar un artículo
        public int Registrar(Articulo obj, out string Mensaje)
        {
            int Data = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarArticulo", conn);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    Data = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Data = 0;
                Mensaje = ex.Message;
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
            }
            return Data;
        }

        // Editar un artículo
        public bool Editar(Articulo obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    string query = "UPDATE Articulos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock WHERE Id = @IdArticulo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdArticulo", obj.Id);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Stock", obj.Stock);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        resultado = true;
                        Mensaje = "Artículo actualizado con éxito.";
                    }
                    else
                    {
                        resultado = false;
                        Mensaje = "No se encontró un artículo con el ID proporcionado.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        // Eliminar un artículo
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    string query = "DELETE FROM Articulos WHERE Id = @IdArticulo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdArticulo", id);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                        Mensaje = "Artículo eliminado correctamente.";
                    }
                    else
                    {
                        resultado = false;
                        Mensaje = "No se encontró un artículo con el ID proporcionado.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
