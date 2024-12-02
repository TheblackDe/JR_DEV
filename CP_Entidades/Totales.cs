using System;

namespace CP_Entidades
{
    // Clase que representa los totales de clientes, pedidos y artículos
    public class Totales
    {
        public int TotalClientes { get; set; }
        public int TotalPedidos { get; set; }
        public int TotalArticulos { get; set; }

        // Constructor sin parámetros
        public Totales() { }

        // Constructor con parámetros
        public Totales(int totalClientes, int totalPedidos, int totalArticulos)
        {
            TotalClientes = totalClientes;
            TotalPedidos = totalPedidos;
            TotalArticulos = totalArticulos;
        }

        // Método ToString para representar los totales como texto
        public override string ToString()
        {
            return $"Total Clientes: {TotalClientes}, Total Pedidos: {TotalPedidos}, Total Artículos: {TotalArticulos}";
        }
    }
}
