using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace FINALRESTO
{
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];//No me andaba fuera del PageLoad
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }
            bool esGerente = Seguridad.esGerente(usuario);
            txtId.Enabled = false;
            ConfirmaEliminacion = false;

            try
            {
                if (!IsPostBack)
                {
                    //Traigo mis tipos de Usuario
                    ddlTipoUsuario.DataSource = Enum.GetValues(typeof(TipoUsuario));
                    ddlTipoUsuario.DataBind();

                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";//Si no es NULL guarda el Id, sino guarda ""

                    if (id != null && id != "")
                    {
                        UsuarioNegocio negocio = new UsuarioNegocio();
                        Usuario seleccionado = (negocio.listar(false, id))[0];//Me trae el User del Id que traje por URL
                        //Usuario seleccionado = (negocio.listar(id))[0];//Me trae el User del Id que traje por URL
                        //Session.Add("usuarioSeleccionado", seleccionado);//Agrego a Session el Usuario

                        txtId.Text = id;
                        txtNombre.Text = seleccionado.Nombre;
                        txtApellido.Text = seleccionado.Apellido;
                        txtDni.Text = seleccionado.Dni;
                        txtPass.Text = seleccionado.Pass;
                        txtFechaNacimiento.Text = seleccionado.FechaNacimiento.ToString(("yyyy-MM-dd"));
                        ddlTipoUsuario.SelectedValue = seleccionado.TipoUsuario.ToString();
                        imagenPerfil.ImageUrl = !string.IsNullOrEmpty(seleccionado.UrlImagen) ? "/Images/" + seleccionado.UrlImagen : "/Images/default.jpg";

                        //Si no es Gerente no puede tocar los atributos
                        txtDni.Enabled = esGerente;
                        txtNombre.Enabled = esGerente;
                        txtApellido.Enabled = esGerente;
                        txtPass.Enabled = esGerente;
                        txtFechaNacimiento.Enabled = esGerente;
                        ddlTipoUsuario.Enabled = esGerente;

                        if (!seleccionado.Activo)
                            btnInactivar.Text = "Reactivar";
                    }
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
            Usuario nuevo = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Dni = txtDni.Text;
                nuevo.Pass = txtPass.Text;
                nuevo.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                nuevo.TipoUsuario = (TipoUsuario)Enum.Parse(typeof(TipoUsuario), ddlTipoUsuario.Text, true);

                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"];
                    nuevo.Id = int.Parse(id);

                    Usuario usuarioModificandose = (negocio.listar(false, id)[0]);

                    if (usuarioModificandose.Dni != nuevo.Dni && negocio.existeDni(nuevo.Dni))
                    {
                        Session.Add("error", "Ese DNI ya se encuentra registrado.");
                        Response.Redirect("Error.aspx", false);
                        return;
                    }

                    //string ruta = Server.MapPath("./Images/");
                    //txtImagenSeleccionada.PostedFile.SaveAs(ruta + "perfil-" + nuevo.Id + ".jpg");
                    //nuevo.UrlImagen = "perfil-" + nuevo.Id + ".jpg";

                    //negocio.modificar(nuevo);

                    string ruta = Server.MapPath("./Images/");
                    if (txtImagenSeleccionada.HasFile)//Ve si tiene una imagen seleccionada
                    {
                        string nuevaImagen = "perfil-" + nuevo.Id + ".jpg";
                        txtImagenSeleccionada.PostedFile.SaveAs(ruta + nuevaImagen);
                        nuevo.UrlImagen = nuevaImagen;
                    }
                    else
                    {
                        nuevo.UrlImagen = usuarioModificandose.UrlImagen;
                    }

                    negocio.modificar(nuevo);

                }
                else//Cuando AGREGO USUARIO
                {
                    if (negocio.existeDni(nuevo.Dni))
                    {
                        Session.Add("error", "Ese DNI ya se encuentra registrado.");
                        Response.Redirect("Error.aspx", false);
                        return;
                    }

                    nuevo.Id = negocio.agregar(nuevo);//Va agregar un USUARIO, SIN IMAGEN

                    if (txtImagenSeleccionada.PostedFile.FileName != "")//Si no elegi imagen no entra aca, asique no me guarda nada en Images, en la base de datos guarda NULL
                    {
                        //Uso la propiedad Server, nosotros estamos dentro de una clase en este momento
                        //MapPath me devuelve desde donde este ejecutando en este momento la ruta fisica, o sea la ruta de FINALRESTO
                        string ruta = Server.MapPath("./Images/");
                        //Ahora guardamos la imagen en Images, PostedFile obtiene los datos del archivo que esta levantando, tiene la referencia del archivo que fue seleccionado
                        txtImagenSeleccionada.PostedFile.SaveAs(ruta + "perfil-" + nuevo.Id + ".jpg");//TENER SOLO JPG EN MI CARPETA

                        //Asignamos UrlImagen para guardarla en el server, antes tenia imagen NULL
                        nuevo.UrlImagen = "perfil-" + nuevo.Id + ".jpg";
                        negocio.modificarImagen(nuevo);
                    }
                }

                //Leo img (~),Actualizo imagen de avatar que esta en el navbar en la MasterPage
                Image img = (Image)Master.FindControl("imgAvatar");//FindControl busca controles en la Master, en este caso busca a imgAvatar, lo casteo a Image porque se que es de ese tipo
                img.ImageUrl = "~/Images/" + nuevo.UrlImagen;

                Response.Redirect("ListaUsuarios.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnActualizarImagen_Click(object sender, EventArgs e)
        {

        }
        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario seleccionado = (Usuario)Session["usuarioSeleccionado"];

                string idRecibido = Request.QueryString["id"];
                if (idRecibido != null)
                {
                    negocio.activarDesactivar(seleccionado.Id, !seleccionado.Activo);//Arreglar activar desactivar, un where id agregar
                    Response.Redirect("ListaUsuarios.aspx", false);
                }
                Response.Redirect("ListaUsuarios.aspx", false);

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
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaUsuarios.aspx", false);
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
