using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace FINALRESTO
{
    public partial class ListaUsuarios : System.Web.UI.Page
    {
        public List<Usuario> ListarUsuarios { get; set; }//Declaro la lista
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            ListarUsuarios = negocio.listar(false);
            repRepetidor.DataSource = ListarUsuarios;
            repRepetidor.DataBind();
            //if (!IsPostBack)
            //{
            //    repRepetidor.DataSource = ListarUsuarios;
            //    repRepetidor.DataBind();
            //}
        }
    }
}