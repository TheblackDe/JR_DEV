using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP_Entidades;

namespace CP_Validacion
{
    // Capa de validación para clientes
    public class CapaValidacionClientes
    {
        // Método para validar los datos de un cliente
        public bool ValidarCliente(Cliente cliente, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación del nombre
            if (string.IsNullOrEmpty(cliente.Nombre))
            {
                mensaje = "El nombre es obligatorio.";
                return false;
            }

            // Validación del apellido
            if (string.IsNullOrEmpty(cliente.Apellido))
            {
                mensaje = "El apellido es obligatorio.";
                return false;
            }

            // Validación de la fecha de nacimiento
            if (cliente.FechaNacimiento == null || cliente.FechaNacimiento > DateTime.Now)
            {
                mensaje = "La fecha de nacimiento no es válida.";
                return false;
            }

            // Validación del estado
            if (cliente.Estado != true && cliente.Estado != false)
            {
                mensaje = "El estado debe ser 'Activo' o 'Inactivo'.";
                return false;
            }

            return true;
        }
    }
}
