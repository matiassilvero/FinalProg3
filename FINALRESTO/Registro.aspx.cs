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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Dni = txtDni.Text;
                usuario.Pass = txtPass.Text;
                usuario.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                usuario.TipoUsuario = TipoUsuario.MESERO;//POR DEFECTO REGISTRA COMO MESERO

                if (negocio.existeDni(usuario.Dni))
                {
                    Session.Add("error", "Ese DNI ya se encuentra registrado.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                usuario.Id = negocio.agregar(usuario);//Va agregar un USUARIO, SIN IMAGEN
                if (txtImagenSeleccionada.PostedFile.FileName != "")//Si no elegi imagen no entra aca, asique no me guarda nada en Images, en la base de datos guarda NULL
                {
                    //Uso la propiedad Server, nosotros estamos dentro de una clase en este momento
                    //MapPath me devuelve desde donde este ejecutando en este momento la ruta fisica, o sea la ruta de FINALRESTO
                    string ruta = Server.MapPath("./Images/");
                    //Ahora guardamos la imagen en Images, PostedFile obtiene los datos del archivo que esta levantando, tiene la referencia del archivo que fue seleccionado
                    txtImagenSeleccionada.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");//TENER SOLO JPG EN MI CARPETA

                    //Asignamos UrlImagen para guardarla en el server, antes tenia imagen NULL
                    usuario.UrlImagen = "perfil-" + usuario.Id + ".jpg";
                    negocio.modificarImagen(usuario);
                }

                Response.Redirect("Login.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error","Error al registrar" + ex.ToString());
            }
        }
    }
}