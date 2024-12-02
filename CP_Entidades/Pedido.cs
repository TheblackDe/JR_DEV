using System;
using System.Collections.Generic;

namespace CP_Entidades
{
    // Clase que representa un pedido, incluyendo su cliente, comentario y detalles
    public class Pedido
    {
        public int IdCliente { get; set; }
        public string Comentario { get; set; }
        public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>(); // Asegurarse de que esté inicializado
    }
}
