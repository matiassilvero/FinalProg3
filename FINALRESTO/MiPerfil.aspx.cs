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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];

            if (!Seguridad.sesionActiva(usuario) || usuario == null)
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }

            try
            {
                if (!IsPostBack)
                {
                    if (usuario != null && !string.IsNullOrEmpty(usuario.UrlImagen))
                        imagenPerfil.ImageUrl = "~/Images/" + usuario.UrlImagen;

                    ddlTipoUsuario.DataSource = Enum.GetValues(typeof(TipoUsuario));
                    ddlTipoUsuario.DataBind();

                    txtId.Text = usuario.Id.ToString();
                    txtDni.Text = usuario.Dni;
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    txtPass.Text = usuario.Pass;
                    txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString("yyyy-MM-dd");
                    ddlTipoUsuario.SelectedValue = usuario.TipoUsuario.ToString();

                    //Si no es Gerente no puede tocar los atributos
                    bool esGerente = Seguridad.esGerente(usuario);

                    txtId.Enabled = esGerente;
                    txtDni.Enabled = esGerente;
                    txtNombre.Enabled = esGerente;
                    txtApellido.Enabled = esGerente;
                    txtPass.Enabled = esGerente;
                    txtFechaNacimiento.Enabled = esGerente;
                    ddlTipoUsuario.Enabled = esGerente;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                if (txtImagenSeleccionada.PostedFile.FileName != "")//Escribir img si se cargó algo
                {
                    string ruta = Server.MapPath("./Images/");//MapPath devuelve la ruta fisica de FINALRESTO

                    txtImagenSeleccionada.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");//Guardamos imagen que levantamos desde la PC
                    usuario.UrlImagen = "perfil-" + usuario.Id + ".jpg";
                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Dni = txtDni.Text;
                usuario.Pass = txtPass.Text;
                usuario.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                usuario.TipoUsuario = (TipoUsuario)Enum.Parse(typeof(TipoUsuario), ddlTipoUsuario.Text, true);
                usuario.Activo = true;

                negocio.modificar(usuario);

                //Leo img (~), Actualizo imagen de avatar que esta en el navbar en la MasterPage
                Image img = (Image)Master.FindControl("imgAvatar");//FindControl busca controles en la Master, en este caso busca a imgAvatar, lo casteo a Image porque se que es de ese tipo
                img.ImageUrl = "~/Images/" + usuario.UrlImagen;//Asigno mi imagen al avatar

                Response.Redirect("ListaUsuarios.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}