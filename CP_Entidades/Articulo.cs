using System;
using System.Collections.Generic;

namespace CP_Entidades
{
    // Clase que representa la tabla Articulos
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

    }
}
