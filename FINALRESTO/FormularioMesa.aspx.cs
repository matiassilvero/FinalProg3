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
    public partial class FormularioMesa : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        public bool reciboId = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            txtId.Enabled = false;
            ConfirmaEliminacion = false;

            try
            {
                if (!IsPostBack)
                {
                    //Traigo mis tipos de Disponibilidad
                    ddlDisponibilidad.DataSource = Enum.GetValues(typeof(Disponibilidad));
                    ddlDisponibilidad.DataBind();
                }

                //Si id vienen con un valor por URL, quiere decir que estamos modificando una Mesa
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" )
                {
                    reciboId = true;
                    MesaNegocio negocio = new MesaNegocio();
                    Mesa seleccionado = (negocio.listar(false,id))[0];

                    Session.Add("mesaSeleccionado", seleccionado);

                    txtId.Text = id;
                    txtCapacidad.Text = seleccionado.Capacidad.ToString();
                    ddlDisponibilidad.SelectedValue = seleccionado.Disponibilidad.ToString();
                    ddlDisponibilidad.Enabled = false;

                    if (!seleccionado.Activo)
                        btnInactivar.Text = "Reactivar";
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Mesa nuevo = new Mesa();
                MesaNegocio negocio = new MesaNegocio();

                nuevo.Capacidad = int.Parse(txtCapacidad.Text);
                nuevo.Disponibilidad = (Disponibilidad)Enum.Parse(typeof(Disponibilidad), ddlDisponibilidad.Text, true);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }

                Response.Redirect("ListaMesas.aspx", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                MesaNegocio negocio = new MesaNegocio();
                Mesa seleccionado = (Mesa)Session["mesaSeleccionado"];

                negocio.activarDesactivar(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("ListaMesas.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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
                    MesaNegocio negocio = new MesaNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaMesas.aspx", false);
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