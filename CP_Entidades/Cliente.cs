using System;

namespace CP_Entidades
{
    // Clase que representa la tabla Clientes
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
