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
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";

            //Usuario usuario = (Usuario)Session["usuario"];
            //lblUsuario.Text = usuario.Nombre;

            //if (!string.IsNullOrEmpty(usuario.UrlImagen))
            //{
            //    imgAvatar.ImageUrl = "~/Images/" + usuario.UrlImagen;
            //}
            //else
            //{
            //    imgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";
            //}

            if (Page is Login || Page is Registro || Page is Home || Page is Error || Page is Ayuda)
            {
                if (Seguridad.sesionActiva(Session["usuario"]))
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblUsuario.Text = usuario.Nombre;

                    if (!string.IsNullOrEmpty(usuario.UrlImagen))
                        imgAvatar.ImageUrl = "~/Images/" + usuario.UrlImagen;
                }
            }
            else
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblUsuario.Text = usuario.Nombre;

                    if (!string.IsNullOrEmpty(usuario.UrlImagen))
                        imgAvatar.ImageUrl = "~/Images/" + usuario.UrlImagen;
                }
            }
            //else
            //{
            //    Response.Redirect("Login.aspx", false);
            //}

            //Si la pagina no es una de estas, lo manda a Loguearse
            //if (!(Page is Login || Page is Registro || Page is Home || Page is Error || Page is Ayuda))
            //{
            //    if (!Seguridad.sesionActiva(Session["usuario"]))
            //    {
            //        Response.Redirect("Login.aspx", false);
            //    }
            //else//Si tenes session abierta, capturo Usuario 
            //{
            //    Usuario usuario = (Usuario)Session["usuario"];
            //    lblUsuario.Text = usuario.Nombre;

            //    if (!string.IsNullOrEmpty(usuario.UrlImagen))
            //        imgAvatar.ImageUrl = "~/Images/" + usuario.UrlImagen;
            //}
            //}
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}