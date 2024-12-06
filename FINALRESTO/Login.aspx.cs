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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario.Dni = txtDni.Text;
                usuario.Pass = txtPass.Text;
                if (negocio.login(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("MiPerfil.aspx", false);
                }
                else
                {
                    Session.Add("error", "Dni o Pass incorrectos");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception)
            {
                Session.Add("error", "Error durante logueo");
                Response.Redirect("Error.aspx");
            }
        }
    }
}