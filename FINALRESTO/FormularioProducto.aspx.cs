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
    public partial class FormularioProducto : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        public bool reciboId = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];//Para ver si el Usuario en session es GERENTE o MESERO
            if (!Seguridad.sesionActiva(usuario))
            {
                Session.Add("error", "No iniciaste sesion");
                Response.Redirect("Error.aspx", false);
            }

            txtId.Enabled = false;
            bool esGerente = Seguridad.esGerente(usuario);
            
            ConfirmaEliminacion = false;

            try
            {
                if (!IsPostBack)
                {
                    //Traigo mis tipos de Producto
                    ddlTipoProducto.DataSource = Enum.GetValues(typeof(TipoProducto));
                    ddlTipoProducto.DataBind();

                    //Si id vienen con un valor por URL, quiere decir que estamos modificando un Producto
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                    if (id != "")
                    {
                        ProductoNegocio negocio = new ProductoNegocio();
                        Producto seleccionado = (negocio.listar(esGerente, id))[0];
                        Session.Add("productoSeleccionado", seleccionado);
                        reciboId = true;

                        txtId.Text = id;
                        txtNombre.Text = seleccionado.Nombre;
                        txtStock.Text = seleccionado.Stock.ToString();
                        txtPrecio.Text = seleccionado.Precio.ToString();
                        ddlTipoProducto.SelectedValue = seleccionado.TipoProducto.ToString();
                        imagenProducto.ImageUrl = !string.IsNullOrEmpty(seleccionado.UrlImagen) ? "/Images/" + seleccionado.UrlImagen : "/Images/default.jpg";

                        //Si no es Gerente no puede tocar los atributos
                        txtNombre.Enabled = esGerente;
                        txtStock.Enabled = esGerente;
                        txtPrecio.Enabled = esGerente;
                        ddlTipoProducto.Enabled = esGerente;

                        if (!seleccionado.Activo)
                            btnInactivar.Text = "Reactivar";
                        //txtUrlImagen_TextChanged(sender, e);
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
            try
            {
                Producto nuevo = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Stock = int.Parse(txtStock.Text);
                nuevo.Precio = float.Parse(txtPrecio.Text);
                nuevo.TipoProducto = (TipoProducto)Enum.Parse(typeof(TipoProducto), ddlTipoProducto.Text, true);

                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"];
                    nuevo.Id = int.Parse(id);

                    Producto productoModificandose = (negocio.listar(false, id)[0]);

                    //string ruta = Server.MapPath("./Images/");
                    //txtImagenSeleccionada.PostedFile.SaveAs(ruta + "producto-" + nuevo.Id + ".jpg");
                    //nuevo.UrlImagen = "producto-" + nuevo.Id + ".jpg";

                    string ruta = Server.MapPath("./Images/");
                    if (txtImagenSeleccionada.HasFile)//Ve si tiene una imagen seleccionada
                    {
                        string nuevaImagen = "producto-" + nuevo.Id + ".jpg";
                        txtImagenSeleccionada.PostedFile.SaveAs(ruta + nuevaImagen);
                        nuevo.UrlImagen = nuevaImagen;
                    }
                    else
                    {
                        nuevo.UrlImagen = productoModificandose.UrlImagen;
                    }

                    negocio.modificar(nuevo);
                }
                else//Cuando AGREGO PRODUCTO
                {
                    nuevo.Id = negocio.agregar(nuevo);//Va agregar un PRODUCTO, SIN IMAGEN

                    if (txtImagenSeleccionada.PostedFile.FileName != "")
                    {
                        string ruta = Server.MapPath("./Images/");
                        //Ahora guardamos la imagen en Images, PostedFile obtiene los datos del archivo que esta levantando, tiene la referencia del archivo que fue seleccionado
                        txtImagenSeleccionada.PostedFile.SaveAs(ruta + "producto-" + nuevo.Id + ".jpg");//TENER SOLO JPG EN MI CARPETA

                        //Asignamos UrlImagen para guardarla en el server, antes tenia imagen NULL
                        nuevo.UrlImagen = "producto-" + nuevo.Id + ".jpg";
                        negocio.modificarImagen(nuevo);
                    }
                }
                Response.Redirect("ListaProductos.aspx", false);
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
                ProductoNegocio negocio = new ProductoNegocio();
                Producto seleccionado = (Producto)Session["productoSeleccionado"];

                negocio.activarDesactivar(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("ListaProductos.aspx", false);
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
                    ProductoNegocio negocio = new ProductoNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaProductos.aspx", false);
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