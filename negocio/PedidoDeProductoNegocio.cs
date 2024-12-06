using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class PedidoDeProductoNegocio
    {
        public List<PedidoDeProducto> listar(string id = "")//Esto permite que LISTAR funcione de 2 maneras, si esta vacío trae TODOS los registros
        {
            List<PedidoDeProducto> lista = new List<PedidoDeProducto>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT id, idPedido, idProducto, cantidad FROM PEDIDOSDEPRODUCTOS";
                if (id != "")
                    comando.CommandText += " WHERE idPedido = " + id; //Esto es para cuando recibo un id por parametro
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    PedidoDeProducto aux = new PedidoDeProducto();
                    aux.Id = (int)lector["id"];
                    aux.IdPedido= (int)lector["idPedido"];
                    aux.IdProducto = (int)lector["idProducto"];
                    aux.Cantidad = (int)lector["cantidad"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PedidoDeProducto> listarPedidosDeProductos(string id = "")//Esto permite que LISTAR funcione de 2 maneras, si esta vacío trae TODOS los registros
        {
            List<PedidoDeProducto> lista = new List<PedidoDeProducto>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT id, idPedido, idProducto, cantidad FROM PEDIDOSDEPRODUCTOS";
                if (id != "")
                    comando.CommandText += " WHERE id = " + id; //Esto es para cuando recibo un id por parametro
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    PedidoDeProducto aux = new PedidoDeProducto();
                    aux.Id = (int)lector["id"];
                    aux.IdPedido = (int)lector["idPedido"];
                    aux.IdProducto = (int)lector["idProducto"];
                    aux.Cantidad = (int)lector["cantidad"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool modificarCantidad(PedidoDeProducto aux)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //Veo Cantidad de Producto en ese PedidoDeProducto
                datos.setearConsulta("SELECT cantidad FROM PEDIDOSDEPRODUCTOS WHERE id = @id");
                datos.setearParametro("@id", aux.Id);
                datos.ejecutarLectura();


                if (datos.Lector.Read())
                {
                    int cantidadActual = (int)datos.Lector["cantidad"];
                    int cantidadARestar = aux.Cantidad;

                    if (cantidadARestar <= cantidadActual)//Si la Cantidad a restar es menor a la Cantidad que tiene el pedido de producto, entro
                    {
                        datos.cerrarConexion();
                        datos = new AccesoDatos();

                        datos.setearConsulta("UPDATE PEDIDOSDEPRODUCTOS SET cantidad = cantidad - @cantidad WHERE id = @id");
                        datos.setearParametro("@id", aux.Id);
                        datos.setearParametro("@cantidad", aux.Cantidad);
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

        public void agregar(PedidoDeProducto nuevo)//Va traer todo de PEDIDOS y PRODUCTOS
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PEDIDOSDEPRODUCTOS(idPedido, idProducto, cantidad)Values(@idPedido, @idProducto, @cantidad)");
                datos.setearParametro("@idPedido", nuevo.IdPedido);
                datos.setearParametro("@idProducto", nuevo.IdProducto);
                datos.setearParametro("@cantidad", nuevo.Cantidad);
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

        public void modificar(PedidoDeProducto pedidoDeProducto)//No le modifico a que PEDIDO esta, modifico producto y cantidad
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET idProducto = @idProducto, cantidad= @cantidad Where id = @id");
                datos.setearParametro("@idProducto", pedidoDeProducto.IdProducto);
                datos.setearParametro("@cantidad", pedidoDeProducto.Cantidad);
                datos.setearParametro("@id", pedidoDeProducto.Id);
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
                datos.setearConsulta("DELETE FROM PEDIDOSDEPRODUCTOS Where id = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
