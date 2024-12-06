using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FINALRESTO
{
    public partial class FormularioPedidoDeProducto : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            txtId.Enabled = false;
            txtIdPedido.Enabled = false;
            txtIdProducto.Enabled = false;
            ConfirmaEliminacion = false;

            try
            {
                if (!IsPostBack)//No lo voy a usar
                {
                }

                //Traigo Id de Producto, SIEMPRE VOY A TRAER UN ID, PORQUE TODOS LOS PRODUCTOS TIENEN
                string idProducto = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (idProducto != "" && !IsPostBack)
                {
                    ProductoNegocio negocio = new ProductoNegocio();
                    Producto productoSeleccionado = (negocio.listar(false, idProducto))[0];

                    //Traigo de Session el ID del Pedido que estoy cargando
                   
                    if (Session["idPedido"] != null)
                    {   //Obtengo ID DEL PEDIDO
                        txtIdPedido.Text = Session["idPedido"].ToString();
                        txtIdProducto.Text = idProducto;
                    }
                    else
                    {
                        Session.Add("error", "No estas agregando el Producto a un Pedido. ");
                        Response.Redirect("Error.aspx", false);
                    }
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
            ProductoNegocio productoNegocio = new ProductoNegocio();

            //Obtengo ID DE PRODUCTO
            string idProducto = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            int idPedido = (int)Session["idPedido"];

            try
            {
                PedidoDeProducto nuevo = new PedidoDeProducto();
                PedidoDeProductoNegocio negocio = new PedidoDeProductoNegocio();

                nuevo.IdPedido = idPedido;
                //nuevo.IdPedido = int.Parse(txtIdPedido.Text);
                nuevo.IdProducto = int.Parse(idProducto);
                nuevo.Cantidad = int.Parse(txtCantidad.Text);

                if(productoNegocio.modificarStock(nuevo)){
                    negocio.agregar(nuevo);
                    Session.Remove("idPedido");
                    Response.Redirect("ListaPedidos.aspx", false);
                }
                else
                {
                    Session.Remove("idPedido");
                    Session.Add("error", "Error con la Cantidad ingresada y el Stock del producto. ");
                    Response.Redirect("Error.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Session.Remove("idPedido");
                Session.Add("error", "Error con la cantidad y el stock del producto" + ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}