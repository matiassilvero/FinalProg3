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
    public partial class EmisionFactura : System.Web.UI.Page
    {
        public int Mesa { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                int idPedido = int.Parse(id);
                if (id != "")
                {
                    PedidoNegocio pedidoNegocio = new PedidoNegocio();
                    Pedido pedido = (pedidoNegocio.listar(id))[0];
                    ProductoNegocio productoNegocio = new ProductoNegocio();
                    FacturaNegocio facturaNegocio = new FacturaNegocio();
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    MesaNegocio mesaNegocio = new MesaNegocio();

                    lblFactura.Text = idPedido.ToString();
                    lblMesa.Text = pedido.IdMesa.ToString();
                    lblFecha.Text = pedido.Fecha.ToString("yyyy-MM-dd HH:mm");
                    lblMesero.Text = usuarioNegocio.obtenerApellido(pedido.IdMesero);
                    float total = facturaNegocio.calcularTotal(idPedido);
                    lblTotal.Text = "$" + total.ToString();

                    DataTable productos = productoNegocio.listarPorIdPedido(idPedido);

                    //Asignar la lista de productos al Repeater
                    repProductos.DataSource = productos;
                    repProductos.DataBind();

                    //Agregar factura a la base de datos
                    Factura nueva = new Factura();

                    nueva.NumeroFactura = idPedido;
                    nueva.Mesa = pedido.IdMesa;
                    nueva.Mesero = pedido.IdMesero;
                    nueva.Fecha = pedido.Fecha;
                    nueva.Importe = total;

                    facturaNegocio.agregar(nueva);

                    //Cambio estado a Pedido
                    pedidoNegocio.factuar(pedido.Id);

                    //Libero mesa
                    mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 1);
                }
            }

        }
    }
}