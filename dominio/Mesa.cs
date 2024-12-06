using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum Disponibilidad
    {
        LIBRE = 1,
        OCUPADA = 2,
        RESERVADA = 3
    }
    public  class Mesa
    {
        public int Id {  get; set; }
        public int Capacidad { get; set; }
        public Disponibilidad Disponibilidad { get; set; }
        public bool Activo { get; set; }
    }
}
