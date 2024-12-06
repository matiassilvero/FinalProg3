using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace FINALRESTO
{
    public partial class ListaMesas : System.Web.UI.Page
    {
        public List<Mesa> Lista {  get; set; } 
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }

            MesaNegocio negocio = new MesaNegocio();
            Lista = negocio.listar(false);

            dgvMesas.DataSource = Lista;
            dgvMesas.DataBind();
        }
    }
}