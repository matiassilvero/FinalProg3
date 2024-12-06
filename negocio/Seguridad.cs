using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user: null;
            if (usuario != null && usuario.Id != 0)
                return true;
            else
                return false;
        }

        public static bool esGerente(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            return usuario != null && usuario.TipoUsuario == TipoUsuario.GERENTE;

            //if (user != null)
            //{
            //    usuario = (Usuario)user;
            //}

            //if (usuario != null && usuario.TipoUsuario == TipoUsuario.GERENTE)
            //{
            //    return true;
            //}

            //return false;
        }
    }
}
