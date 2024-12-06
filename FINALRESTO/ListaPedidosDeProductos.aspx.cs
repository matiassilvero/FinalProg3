using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;
using static System.Collections.Specialized.BitVector32;

namespace FINALRESTO
{
    public partial class ListaPedidosDeProductos : System.Web.UI.Page
    {
        public List<PedidoDeProducto> Lista { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            //string idPedido = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            //if (idPedido != "" && !IsPostBack)
            //{
            //    PedidoDeProductoNegocio negocio = new PedidoDeProductoNegocio();
            //    Lista = negocio.listar(idPedido);

            //}
            //else
            //{
            //    PedidoDeProductoNegocio negocio = new PedidoDeProductoNegocio();
            //    Lista = negocio.listar();

            //}
            //    dgvPedidosDeProductos.DataSource = Lista;
            //    dgvPedidosDeProductos.DataBind();

            PedidoDeProductoNegocio negocio = new PedidoDeProductoNegocio();
            string idPedido = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            if (idPedido != "" && !IsPostBack)
            {
                Lista = negocio.listar(idPedido);
            }
            else
            {
                Lista = negocio.listar();
            }

            //Session["pedidoAQuitarProducto"] = idPedido;

            dgvPedidosDeProductos.DataSource = Lista;
            dgvPedidosDeProductos.DataBind();

        }
    }
}

