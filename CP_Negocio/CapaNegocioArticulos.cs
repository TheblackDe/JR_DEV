using System;
using System.Collections.Generic;
using CP_Datos;  // Capa de acceso a datos para artículos
using CP_Entidades;  // Capa de entidades (modelos) que contiene la clase Articulo

namespace CP_Negocio
{
    // Capa de negocio para la gestión de artículos
    public class CapaNegocioArticulos
    {
        // Instancia de la capa de datos
        private CapaDatosArticulos _capaDatosArticulos = new CapaDatosArticulos();

        // Método para listar artículos
        public List<Articulo> Listar()
        {
            return _capaDatosArticulos.Listar();
        }

        // Método para registrar un artículo
        public int Registrar(Articulo articulo, out string mensaje)
        {
            return _capaDatosArticulos.Registrar(articulo, out mensaje);
        }

        // Método para editar un artículo
        public bool Editar(Articulo articulo, out string mensaje)
        {
            return _capaDatosArticulos.Editar(articulo, out mensaje);
        }

        // Método para eliminar un artículo
        public bool Eliminar(int id, out string mensaje)
        {
            return _capaDatosArticulos.Eliminar(id, out mensaje);
        }
    }
}
