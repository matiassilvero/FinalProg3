using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace FINALRESTO
{
    public partial class ListaPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (!Seguridad.sesionActiva(usuario))
                {
                    Session.Add("error", "No iniciaste sesión");
                    Response.Redirect("Error.aspx", false);
                }

                PedidoNegocio negocio = new PedidoNegocio();
                DataTable tablaPedidos = negocio.obtenerPedidos();

                dgvPedidos.DataSource = tablaPedidos;
                dgvPedidos.DataBind(); //Esto dispara el evento RowDataBound
            }
        }
        protected void dgvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();

                //Traigo Id de la fila actual
                int idPedido = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "id"));

                //Encuentro el Repeater dentro de la fila
                Repeater repProductos = (Repeater)e.Row.FindControl("repProductos");

                if (repProductos != null)
                {
                    PedidoNegocio negocio = new PedidoNegocio();
                    DataTable tablaPedidos = productoNegocio.listarPorIdPedido(idPedido);//Traigo PRODUCTOS de ese Pedido

                    repProductos.DataSource = tablaPedidos; 
                    repProductos.DataBind();
                }
            }
        }

        protected void dgvPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPedidos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioPedido.aspx?id=" + id);
        }
        protected void btnVer_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string idPedido = button.CommandArgument;

            Response.Redirect("FormularioPedido.aspx?id= " + idPedido, false);
        }

        //protected void facturar_Click(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    string idPedido = button.CommandArgument;

        //    Response.Redirect("Factura.aspx?id= "+ idPedido,false);
        //}
    }
}