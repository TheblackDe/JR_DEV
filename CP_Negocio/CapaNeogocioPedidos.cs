using System;
using System.Collections.Generic;
using CP_Entidades;
using CP_Datos;

namespace CP_Negocio
{
    // Capa de negocio para gestionar pedidos
    public class CapaNegocioPedidos
    {
        // Instancia privada de CapaDatosPedidos
        private CapaDatosPedidos _capaDatosPedidos = new CapaDatosPedidos();

        // Método para insertar un pedido
        public int InsertarPedido(Pedido pedido, out string mensaje)
        {
            return _capaDatosPedidos.InsertarPedido(pedido, out mensaje);
        }
    }
}
