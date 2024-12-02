using System;
using System.Collections.Generic;
using CP_Datos;
using CP_Entidades;
using CP_Validacion;

namespace CP_Negocio
{
    // Capa de negocio para la gestión de clientes
    public class CapaNegocioClientes
    {
        private CapaDatosClientes _capaDatosClientes = new CapaDatosClientes();
        private CapaValidacionClientes _capaValidacionClientes = new CapaValidacionClientes();

        // Método para listar clientes
        public List<Cliente> Listar()
        {
            return _capaDatosClientes.Listar();
        }

        // Método para registrar un cliente con validación
        public int Registrar(Cliente cliente, out string mensaje)
        {
            if (!_capaValidacionClientes.ValidarCliente(cliente, out mensaje))
            {
                return 0;
            }
            return _capaDatosClientes.Registrar(cliente, out mensaje);
        }

        // Método para editar un cliente con validación
        public bool Editar(Cliente cliente, out string mensaje)
        {
            if (!_capaValidacionClientes.ValidarCliente(cliente, out mensaje))
            {
                return false;
            }
            return _capaDatosClientes.Editar(cliente, out mensaje);
        }

        // Método para eliminar un cliente
        public bool Eliminar(int Id, out string mensaje)
        {
            return _capaDatosClientes.Eliminar(Id, out mensaje);
        }

        // Método para buscar un cliente por nombre y apellido
        public Cliente BuscarClientePorNombreApellido(string nombre, string apellido)
        {
            Console.WriteLine($"Recibiendo datos: Nombre = {nombre}, Apellido = {apellido}");
            return _capaDatosClientes.BuscarClientePorNombreApellido(nombre, apellido);
        }
    }
}
