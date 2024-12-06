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
    public partial class QuitarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)//No lo voy a usar
            {
            }

            try
            {
                //Traigo Id de PEDIDODEPRODUCTO
                string idPedidoDeProducto = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (idPedidoDeProducto != "")
                {
                    //ProductoNegocio negocio = new ProductoNegocio();
                    //Producto productoSeleccionado = (negocio.listar(false, idProducto))[0];
                    
                    //Traigo el PedidoDeProducto, con esto obtengo todos los datos que necesito
                    PedidoDeProductoNegocio pedidoDeProductoNegocio = new PedidoDeProductoNegocio();
                    PedidoDeProducto pedidoDeProducto = (pedidoDeProductoNegocio.listarPedidosDeProductos(idPedidoDeProducto))[0];

                    txtIdPedido.Text = pedidoDeProducto.IdPedido.ToString();
                    txtIdPedido.Enabled = false;

                    txtIdProducto.Text = pedidoDeProducto.IdProducto.ToString();
                    txtIdProducto.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtengo ID de PEDIDODEPRODUCTO
                string idPedidoDeProducto = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                PedidoDeProductoNegocio pedidoDeProductoNegocio = new PedidoDeProductoNegocio();
                PedidoDeProducto pedidoDeProducto = (pedidoDeProductoNegocio.listarPedidosDeProductos(idPedidoDeProducto))[0];

                ProductoNegocio productoNegocio = new ProductoNegocio();

                //int cantidadAQuitar = int.Parse(txtCantidad.Text);//ACA LA CANTIDAD QUE QUIERO QUITAR DE ESE PRODUCTO

                PedidoDeProducto aux = new PedidoDeProducto();

                aux.Id = pedidoDeProducto.Id;
                aux.IdPedido = pedidoDeProducto.IdPedido;
                aux.IdProducto = pedidoDeProducto.IdProducto;
                aux.Cantidad = int.Parse(txtCantidad.Text);

                //Para modificar mis Productos
                int idProducto = aux.IdProducto;
                int cantidadASumar = aux.Cantidad;


                if (pedidoDeProductoNegocio.modificarCantidad(aux))
                {
                    //negocio.agregar(nuevo);
                    //Session.Remove("pedidoAQuitarProducto");

                    //Devuelvo esos Productos quitados a mi Stock
                    productoNegocio.sumarStock(idProducto, cantidadASumar);

                    Session.Remove("idPedido");

                    Response.Redirect("ListaPedidos.aspx", false);
                }
                else
                {
                    //Session.Remove("pedidoAQuitarProducto");
                    Session.Remove("idPedido");
                    Session.Add("error", "Error con la Cantidad de productos a quitar. ");
                    Response.Redirect("Error.aspx", false);
                    //return;
                }
            }
            catch (Exception ex)
            {
                //Session.Remove("pedidoAQuitarProducto");
                Session.Add("error", "Error con la Cantidad a quitar. " + ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
    
}