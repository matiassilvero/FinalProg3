using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using negocio;
using System.Data;
using System.Globalization;

namespace negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar(bool esGerente, string id = "")//Esto permite que LISTAR funcione de 2 maneras, si esta vacío, trae TODOS los registros
        {
            List<Producto> lista = new List<Producto>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = esGerente ? "SELECT id, nombre, stock, precio, urlImagen, tipoProducto, activo FROM PRODUCTOS"
                                                : "SELECT id, nombre, stock, precio, urlImagen, tipoProducto, activo FROM PRODUCTOS WHERE stock > 0";
                if (id != "" && esGerente)
                {
                    comando.CommandText += " WHERE id = " + id;
                }
                if (id != "" && !esGerente)
                {
                    comando.CommandText += " AND id = " + id;
                }
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)lector["id"];
                    aux.Nombre = (string)lector["nombre"];
                    aux.Stock = (int)lector["stock"];
                    //aux.Precio = (float)lector["precio"];
                    aux.Precio = lector["precio"] != DBNull.Value ? Convert.ToSingle(lector["precio"]) : 0f; //Maneja nulos, si no hacia esto no me mostraba los productos
                    //if (!(lector["urlImagen"] is DBNull))
                    //    aux.UrlImagen = (string)lector["urlImagen"];
                    aux.UrlImagen = (lector["urlImagen"] is DBNull || (string)lector["urlImagen"] == "") ? "default.jpg" : (string)lector["urlImagen"];
                    aux.TipoProducto = (TipoProducto)(int)lector["tipoProducto"];
                    aux.Activo = bool.Parse(lector["activo"].ToString());

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void sumarStock(int idProducto, int cantidadASumar)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("UPDATE PRODUCTOS SET stock = stock + @cantidadASumar WHERE id = @idProducto");
            datos.setearParametro("@idProducto", idProducto);
            datos.setearParametro("@cantidadASumar", cantidadASumar);
            datos.ejecutarAccion();

            datos.cerrarConexion();
        }
        public bool modificarStock(PedidoDeProducto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //Veo Stock
                datos.setearConsulta("SELECT stock FROM Productos WHERE id = @idProducto");
                datos.setearParametro("@idProducto", nuevo.IdProducto);
                datos.ejecutarLectura();

                
                if (datos.Lector.Read())
                {
                    int stockDisponible = (int)datos.Lector["stock"];

                    if (stockDisponible >= nuevo.Cantidad)//Si el Stock supera la Cantidad solicitada, entro
                    {
                        datos.cerrarConexion();
                        datos = new AccesoDatos();

                        datos.setearConsulta("UPDATE PRODUCTOS SET stock = stock - @cantidad WHERE id = @idProducto");
                        datos.setearParametro("@idProducto", nuevo.IdProducto);
                        datos.setearParametro("@cantidad", nuevo.Cantidad);
                        datos.ejecutarAccion();

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable listarPorIdPedido(int idPedido)
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            DataTable dataTable = new DataTable();

            try
            {
                datos.setearConsulta("SELECT PRO.nombre, PP.cantidad, PRO.precio,(PRO.precio * PP.cantidad) AS Subtotal, PRO.tipoProducto " +
                   "FROM PEDIDOS P " +
                   "JOIN PEDIDOSDEPRODUCTOS PP ON P.id = PP.idPedido " +
                   "JOIN PRODUCTOS PRO ON PP.idProducto = PRO.id " +
                   "WHERE P.Id = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                dataTable.Load(datos.Lector);//Llenamos la DataTable

                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PRODUCTOS(nombre, stock, precio, urlImagen, tipoProducto, activo) " +
                    "output inserted.id VALUES(@nombre, @stock, @precio, @urlImagen, @tipoProducto, 1)");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@stock", nuevo.Stock);
                datos.setearParametro("@precio", nuevo.Precio);
                //datos.setearParametro("@urlImagen", nuevo.UrlImagen);
                datos.setearParametro("@urlImagen", (object)nuevo.UrlImagen ?? DBNull.Value);//Por si pasamos NULL
                datos.setearParametro("@tipoProducto", nuevo.TipoProducto);
                return datos.ejecutarAccionScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET nombre = @nombre, stock = @stock, precio = @precio, urlImagen = @urlImagen, tipoProducto = @tipoProducto Where id = @id");
                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@stock", producto.Stock);
                datos.setearParametro("@precio", producto.Precio);
                datos.setearParametro("@urlImagen", producto.UrlImagen);
                //Porque TipoProducto es ENUM
                datos.setearParametro("@tipoProducto", (int)producto.TipoProducto);
                datos.setearParametro("@id", producto.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificarImagen(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET urlImagen = @urlImagen WHERE id = @id");
                //datos.setearParametro("@urlImagen", usuario.UrlImagen);
                datos.setearParametro("@urlImagen", (object)producto.UrlImagen ?? DBNull.Value);//Por si pasamos NULL
                datos.setearParametro("@id", producto.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM PRODUCTOS Where id = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
        public void activarDesactivar(int Id, bool Activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE PRODUCTOS SET activo = @activo Where id = @id");
                datos.setearParametro("@id", Id);
                datos.setearParametro("@activo", Activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
