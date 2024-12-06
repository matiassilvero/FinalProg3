using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using static System.Collections.Specialized.BitVector32;

namespace negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> listar(string id = "")//Esto permite que LISTAR funcione de 2 maneras, si esta vacío, trae TODOS los registros
        {
            List<Pedido> lista = new List<Pedido>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT id, idMesa, idMesero, fecha, estado FROM PEDIDOS";
                if (id != "")
                    comando.CommandText += " WHERE id = " + id;
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)lector["id"];
                    aux.IdMesa = (int)lector["idMesa"];
                    aux.IdMesero = (int)lector["idMesero"];
                    aux.Fecha = (DateTime)lector["fecha"];
                    aux.Estado = (Estado)(int)lector["estado"];

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

        //public DataTable obtenerPedidos()//Usamos una tabla temporal, DataTable
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    //DataTable es una clase de System.Data, una tabla temporal en memoria.
        //    //tiene filas (DataRow) y columnas (DataColumn)
        //    //herramienta de ADO.NET, interactua con la base de datos
        //    DataTable dataTable = new DataTable();

        //    try
        //    {
        //        datos.setearConsulta("SELECT P.id, P.fecha, P.idMesa, U.apellido, " +
        //            "PRO.nombre AS Producto, PP.cantidad AS Cantidad, PRO.precio AS PrecioUnitario, " +
        //            "(PP.cantidad * PRO.precio) AS Subtotal, " +
        //            "CASE WHEN P.estado = 1 THEN 'EN PREPARACION' " +
        //                 "WHEN P.estado = 2 THEN 'ENTREGADO' " +
        //                 "WHEN P.estado = 3 THEN 'CANCELADO' " +
        //                 "WHEN P.estado = 4 THEN 'FACTURADO' " +
        //            "END AS estado " +
        //            "FROM PEDIDOS P " +
        //            "LEFT JOIN USUARIOS U ON P.idMesero = U.id " +
        //            "LEFT JOIN PEDIDOSDEPRODUCTOS PP ON P.id = PP.idPedido " +
        //            "LEFT JOIN PRODUCTOS PRO ON PP.idProducto = PRO.id " +
        //            "ORDER BY P.id");
        //        datos.ejecutarLectura();

        //        dataTable.Load(datos.Lector);//Llenamos la DataTable

        //        if (dataTable.Rows.Count == 0)// Validamos si no hay filas en el DataTable
        //        {
        //            Console.WriteLine("No se encontraron pedidos.");
        //            return null;
        //        }

        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //        return null;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}
        public DataTable obtenerPedidos()//Usamos una tabla temporal, DataTable
        {
            AccesoDatos datos = new AccesoDatos();
            //DataTable es una clase de System.Data, una tabla temporal en memoria.
            //tiene filas (DataRow) y columnas (DataColumn)
            //herramienta de ADO.NET, interactua con la base de datos
            DataTable dataTable = new DataTable();

            try
            {//STRING_AGG combina filas en una sola, separadas por algo. CONCAT une valores en una sola cadena con formato
                datos.setearConsulta("SELECT P.id, P.fecha, P.idMesa, U.apellido, " +
                    "CASE " +
                    "WHEN P.estado = 1 THEN 'EN PREPARACION' " +
                    "WHEN P.estado = 2 THEN 'ENTREGADO' " +
                    "WHEN P.estado = 3 THEN 'CANCELADO' " +
                    "WHEN P.estado = 4 THEN 'FACTURADO' " +
                    "END AS Estado, " +
                    "STRING_AGG( CONCAT(PRO.nombre, ' x', PP.cantidad, ' ($', PRO.precio, ' c/u)'), ', ') AS Productos " +
                    "FROM PEDIDOS P " +
                    "LEFT JOIN USUARIOS U ON P.idMesero = U.id " +
                    "LEFT JOIN PEDIDOSDEPRODUCTOS PP ON P.id = PP.idPedido " +
                    "LEFT JOIN PRODUCTOS PRO ON PP.idProducto = PRO.id " +
                    "GROUP BY P.id, P.fecha, P.idMesa, U.apellido, P.estado " +
                    "ORDER BY P.id");
                datos.ejecutarLectura();

                dataTable.Load(datos.Lector);//Llenamos la DataTable

                if (dataTable.Rows.Count == 0)// Validamos si no hay filas en el DataTable
                {
                    Console.WriteLine("No se encontraron pedidos.");
                    return null;
                }

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

        public int agregar(Pedido nuevo)//Agregar y que devuelva el int del Id
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PEDIDOS(idMesa, idMesero, fecha, estado) " +
                    "output inserted.id Values(@idMesa, @idMesero, @fecha, @estado)");
                datos.setearParametro("@idMesa", nuevo.IdMesa);
                datos.setearParametro("@idMesero", nuevo.IdMesero);
                datos.setearParametro("@fecha", nuevo.Fecha);
                datos.setearParametro("@estado", nuevo.Estado);
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

        public void modificar(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PEDIDOS SET idMesa = @idMesa, idMesero = @idMesero, fecha = @fecha, estado = @estado Where id= @id");
                datos.setearParametro("@idMesa", pedido.IdMesa);
                datos.setearParametro("@idMesero", pedido.IdMesero);
                datos.setearParametro("@fecha", pedido.Fecha);
                datos.setearParametro("@estado", pedido.Estado);
                datos.setearParametro("@id", pedido.Id);
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
        public void facturar(int id, Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PEDIDOS SET estado = @estado Where id= @id");
                datos.setearParametro("@estado", 4);
                datos.setearParametro("@id", pedido.Id);
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
                datos.setearConsulta("DELETE FROM PEDIDOS Where id = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void factuar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PEDIDOS SET estado = @estado Where id= @id");
                datos.setearParametro("@estado", 4);
                datos.setearParametro("@id", id);
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
    }
}

