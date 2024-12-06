using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FINALRESTO
{
    public partial class FormularioPedido : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        public MesaNegocio mesaNegocio = new MesaNegocio();
        public UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            bool esGerente = Seguridad.esGerente(usuario);
            ConfirmaEliminacion = false;

            try
            {
                if (!IsPostBack)
                {
                    //Traigo mis Mesas disponibles
                    cargarMesasDisponibles();
                    //Traigo mis Meseros 
                    cargarMeseros();
                    //Fecha y Hora actual
                    txtFechaHora.Text = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
                    //Traigo mis tipos de Estado
                    ddlEstado.DataSource = Enum.GetValues(typeof(Estado));
                    ddlEstado.DataBind();
                }

                //Si Id viene con un valor por URL, quiere decir que estamos modificando un Pedido
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "")
                {
                    PedidoNegocio negocio = new PedidoNegocio();
                    Pedido pedido = (negocio.listar(id))[0];
                    Session["pedido"] = pedido;
                    Session["idPedido"] = pedido.Id;

                    txtId.Text = id;
                    txtId.Enabled = false;

                    //cargarMesasDisponibles(pedido.IdMesa.ToString());
                    //ddlIdMesa.SelectedValue = pedido.IdMesa.ToString();
                    ddlIdMesa.Enabled = false;

                    ddlIdMesero.Enabled = false;

                    //txtFechaHora.Text = pedido.Fecha.ToString("yyyy-MM-ddTHH:mm");
                    txtFechaHora.Enabled = false;
                    

                    ddlEstado.SelectedValue = pedido.Estado.ToString();
                }
                else//Si agrego PEDIDO NUEVO
                {
                    //Cargo un Pedido y lo guardo en Session
                    //Tiene todo MENOS ID
                    Pedido pedido = new Pedido();
                    if (ddlIdMesa.Items.Count == 0)
                    {
                        Session.Add("error", "No hay Mesas disponibles!");
                        Response.Redirect("Error.aspx", false);
                        return;
                    }
                    else
                    {
                        pedido.IdMesa = int.Parse(ddlIdMesa.SelectedValue);
                    }

                    if (ddlIdMesero.Items.Count == 0)
                    {
                        Session.Add("error", "No hay Meseros disponibles!");
                        Response.Redirect("Error.aspx", false);
                        return;
                    }

                    if (esGerente)
                    {
                        pedido.IdMesero = int.Parse(ddlIdMesero.SelectedValue);
                    }
                    else
                    {
                        ddlIdMesero.Enabled = false;
                        ddlIdMesero.SelectedValue = usuario.Id.ToString();
                        pedido.IdMesero = usuario.Id;
                    }
                    //pedido.IdMesero = int.Parse(ddlIdMesero.SelectedValue);
                    pedido.Fecha = DateTime.Parse(txtFechaHora.Text);
                    pedido.Estado = (Estado)Enum.Parse(typeof(Estado), ddlEstado.SelectedValue);
                    Session["pedido"] = pedido;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }
        protected void agregarProducto_Click(object sender, EventArgs e)
        {
            string idRecibido = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            Pedido pedido = (Pedido)Session["pedido"];
            PedidoNegocio negocio = new PedidoNegocio();

            if (idRecibido != "")//Modificando Pedido
            //if (pedido.Id == 0) //Si el Id es 0 es porque es un PEDIDO NUEVO
            {
                //Libero/Ocupo MESAS
                if ((int)pedido.Estado == 1 || (int)pedido.Estado == 2)//Estados: 1 EnPREPARACION, 2 ENTREGADO, 3 CANCELADO, 4 FACTURADO

                {
                    mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 2);//Disponibilidad: 1 LIBRE, 2 OCUPADA, 3 RESERVADA
                }
                else
                {
                    mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 1);
                }
                Response.Redirect("ListaProductos.aspx?idPedido=" + idRecibido, false);
            }
            else
            {
                //Crea en la base de datos el PEDIDO y DEVUELVE SU ID
                //Ahora tiene todos los datos el Pedido
                pedido.Id = negocio.agregar(pedido);

                //Libero/Ocupo MESAS
                if ((int)pedido.Estado == 1 || (int)pedido.Estado == 2)//Estados: 1 EnPREPARACION, 2 ENTREGADO, 3 CANCELADO, 4 FACTURADO
                {
                    mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 2);//Disponibilidad: 1 LIBRE, 2 OCUPADA, 3 RESERVADA
                }
                else
                {
                    mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 1);
                }

                Session["idPedido"] = pedido.Id;
                Response.Redirect("ListaProductos.aspx?idPedido=" + pedido.Id, false);//Envío IdPedido por URL
                //Session["pedido"] = pedido;
            }

            //int idPedido = pedido.Id;
            //Response.Redirect("ListaProductos.aspx?idPedido=" + idPedido, false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    Pedido pedido = new Pedido();
                    PedidoNegocio pedidoNegocio = new PedidoNegocio();
                    MesaNegocio mesaNegocio = new MesaNegocio();

                    pedido.IdMesa = int.Parse(ddlIdMesa.Text);
                    pedido.IdMesero = int.Parse(ddlIdMesero.Text);
                    pedido.Fecha = DateTime.Parse(txtFechaHora.Text);
                    pedido.Estado = (Estado)Enum.Parse(typeof(Estado), ddlEstado.Text, true);

                    pedido.Id = int.Parse(txtId.Text);

                    //Libero / Ocupo MESAS
                    if ((int)pedido.Estado == 1 || (int)pedido.Estado == 2)
                    {//Estados: 1 EnPREPARACION, 2 ENTREGADO, 3 CANCELADO, 4 FACTURADO
                        mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 2);//Disponibilidad: 1 LIBRE, 2 OCUPADA, 3 RESERVADA
                    }
                    else
                    {
                        mesaNegocio.modificarDisponibilidad(pedido.IdMesa, 1);
                    }

                    pedidoNegocio.modificar(pedido);
                }

                Response.Redirect("ListaPedidos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Erro en btnAceptar en FormPedido " + ex.Message);
                Response.Redirect("Error.aspx");
            }
        }
        private void cargarMesasDisponibles(string idMesa = "")
        {
            try
            {
                string idMesaRecibido = idMesa;
                if (idMesaRecibido != "")
                {
                    List<Mesa> lista = mesaNegocio.listar(false, idMesaRecibido);
                    ddlIdMesa.DataSource = lista;
                    ddlIdMesa.DataTextField = "Id"; //Campo que se muestra
                    ddlIdMesa.DataValueField = "Id"; //Valor asociado 
                    ddlIdMesa.DataBind();
                }
                else
                {
                    List<Mesa> lista = mesaNegocio.listar(true);
                    ddlIdMesa.DataSource = lista;
                    ddlIdMesa.DataTextField = "Id"; //Campo que se muestra
                    ddlIdMesa.DataValueField = "Id"; //Valor asociado 
                    ddlIdMesa.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarMeseros()
        {
            try
            {
                List<Usuario> lista = usuarioNegocio.listar(true);

                ddlIdMesero.DataSource = lista;
                ddlIdMesero.DataTextField = "apellido"; //Campo que se muestra
                ddlIdMesero.DataValueField = "Id"; //Valor asociado
                ddlIdMesero.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;

        }
        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    PedidoNegocio negocio = new PedidoNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaPedidos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }


    }
}