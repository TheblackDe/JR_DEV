using System;
using System.Collections.Generic;
using CP_Entidades;
using CP_Datos;

namespace CP_Negocio
{
    // Capa de negocio para gestionar el historial de ventas
    public class CapaNegocioHistorialVenta
    {
        private CapaDatosHistorialVenta _repositorio;

        // Constructor que inicializa el repositorio
        public CapaNegocioHistorialVenta()
        {
            _repositorio = new CapaDatosHistorialVenta();
        }

        // Método para obtener el historial de ventas con parámetros opcionales
        public List<HistorialVenta> ObtenerHistorialVentas(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idPedido = null)
        {
            try
            {
                List<HistorialVenta> historialVentas = _repositorio.ObtenerHistorialVentas(fechaInicio, fechaFin, idPedido);
                return historialVentas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la capa de negocio: {ex.Message}");
                return new List<HistorialVenta>(); // Retorna una lista vacía en caso de error
            }
        }
    }
}
