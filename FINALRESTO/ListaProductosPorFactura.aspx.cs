using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FINALRESTO
{
    public partial class ListaProductosPorFactura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }

            ProductoNegocio productoNegocio = new ProductoNegocio();
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            int idPedido = int.Parse(id);

            DataTable dataTable = productoNegocio.listarPorIdPedido(idPedido);

            dgvProductos.DataSource = dataTable;
            dgvProductos.DataBind();
        }
    }
}