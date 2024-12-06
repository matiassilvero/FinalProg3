using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum TipoUsuario
    {
        GERENTE = 1,
        MESERO = 2
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Pass { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string UrlImagen { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool Activo { get; set; }

    }
}
