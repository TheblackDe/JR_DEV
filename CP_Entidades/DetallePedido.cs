using System;

namespace CP_Entidades
{
    // Clase que representa la tabla DetallePedidos
    public class DetallePedido
    {
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalLinea { get; set; }
    }
}
