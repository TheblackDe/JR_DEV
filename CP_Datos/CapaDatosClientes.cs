using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using CP_Entidades;

namespace CP_Datos
{
    public class CapaDatosClientes
    {
        // Listar clientes
        public List<Cliente> Listar()
        {
            List<Cliente> Lista = new List<Cliente>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    conn.Open();
                    string query = "SELECT Id, Nombre, Apellido, Estado, CONVERT(varchar, FechaNacimiento, 23) AS FechaNacimiento FROM Clientes";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lista.Add(new Cliente()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"])
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

        // Registrar cliente
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int Data = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", conn);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("FechaNacimiento", obj.FechaNacimiento);
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

        // Editar cliente
        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    string query = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Estado = @Estado, FechaNacimiento = @FechaNacimiento WHERE Id = @IdCliente";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCliente", obj.Id);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        resultado = true;
                        Mensaje = "Cliente actualizado con éxito.";
                    }
                    else
                    {
                        resultado = false;
                        Mensaje = "No se encontró un cliente con el ID proporcionado.";
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

        // Eliminar cliente
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    string query = "UPDATE Clientes SET Estado = 0 WHERE Id = @IdCliente";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCliente", id);

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                        Mensaje = "Cliente marcado como eliminado correctamente.";
                    }
                    else
                    {
                        resultado = false;
                        Mensaje = "No se encontró un cliente con el ID proporcionado.";
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

        // Buscar cliente por nombre y apellido
        public Cliente BuscarClientePorNombreApellido(string nombre, string apellido)
        {
            Cliente cliente = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conn.cn))
                {
                    conn.Open();

                    string query = "SELECT TOP 1 Id, Nombre, Apellido, Estado, FechaNacimiento " +
                                   "FROM Clientes " +
                                   "WHERE Nombre LIKE @Nombre AND Apellido LIKE @Apellido AND Estado = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = "%" + nombre + "%";
                        cmd.Parameters.Add("@Apellido", SqlDbType.NVarChar).Value = "%" + apellido + "%";

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Cliente()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                                    FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar cliente: {ex.Message}");
            }
            return cliente;
        }
    }
}
