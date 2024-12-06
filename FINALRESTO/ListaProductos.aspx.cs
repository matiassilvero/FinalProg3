using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FINALRESTO
{
    public partial class ListaProductos : System.Web.UI.Page
    {
        public List<Producto> Lista { get; set; }
        public bool recibiIdPedido = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Para ver si el Usuario en session es GERENTE o MESERO
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            bool esGerente = Seguridad.esGerente(usuario);

            string idPedido = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(idPedido))
                recibiIdPedido = true;

            ProductoNegocio negocio = new ProductoNegocio();
            Lista = negocio.listar(esGerente);//Si es gerente ve TODOS los productos

            if (!IsPostBack)
            {
                repRepetidor.DataSource = Lista;
                repRepetidor.DataBind();
            }


        }
    }
}