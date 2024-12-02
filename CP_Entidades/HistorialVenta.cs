using System;

namespace CP_Entidades
{
    // Clase que representa el historial de ventas
    public class HistorialVenta
    {
        public DateTime FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int IdPedido { get; set; }

        public HistorialVenta(DateTime fechaVenta, string cliente, string producto, decimal precio, int cantidad, decimal total, int idPedido)
        {
            FechaVenta = fechaVenta;
            Cliente = cliente;
            Producto = producto;
            Precio = precio;
            Cantidad = cantidad;
            Total = total;
            IdPedido = idPedido;
        }
    }
}
