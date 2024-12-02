using System;
using CP_Datos;
using CP_Entidades;

namespace CP_Negocio
{
    // Capa de negocio para gestionar los totales
    public class CapaNegocioTotales
    {
        private readonly CapaDatosTotales datosTotales;

        // Constructor que inicializa la capa de datos
        public CapaNegocioTotales()
        {
            datosTotales = new CapaDatosTotales();
        }

        // Método para obtener los totales con validaciones de negocio
        public Totales ObtenerTotales()
        {
            // Llamar al método de la capa de datos
            Totales totales = datosTotales.ObtenerTotales();

            // Validaciones de negocio
            if (totales.TotalClientes < 0 || totales.TotalPedidos < 0 || totales.TotalArticulos < 0)
            {
                throw new Exception("Los totales no pueden ser negativos. Verifica los datos de la base.");
            }

            // Mensaje informativo si no hay registros
            if (totales.TotalClientes == 0 && totales.TotalPedidos == 0 && totales.TotalArticulos == 0)
            {
                Console.WriteLine("No hay datos registrados actualmente.");
            }

            return totales;
        }
    }
}
