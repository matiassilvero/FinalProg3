using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class FacturaNegocio
    {
        public void agregar(Factura nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO FACTURAS(numeroFactura, mesa, mesero, fecha, importe) " +
                                    "Values(@numeroFactura, @mesa, @mesero, @fecha, @importe)");
                datos.setearParametro("@numeroFactura", nuevo.NumeroFactura);
                datos.setearParametro("@mesa", nuevo.Mesa);
                datos.setearParametro("@mesero", nuevo.Mesero);
                datos.setearParametro("@fecha", nuevo.Fecha);
                datos.setearParametro("@importe", nuevo.Importe);
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

        public DataTable listarFacturas()
        {
            List<Factura> lista = new List<Factura>();
            AccesoDatos datos = new AccesoDatos();
            DataTable dataTable = new DataTable();

            try
            {
                datos.setearConsulta("SELECT id , numeroFactura, mesa, mesero, fecha, importe FROM FACTURAS");
                datos.ejecutarLectura();

                dataTable.Load(datos.Lector);//Llenamos la DataTable

                return dataTable;

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
        public float calcularTotal(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            DataTable dataTable = new DataTable();
            float total = 0;

            try
            {
                datos.setearConsulta("SELECT SUM(PP.cantidad * PRO.precio) AS Total " +
                   "FROM PEDIDOS P " +
                   "JOIN PEDIDOSDEPRODUCTOS PP ON P.id = PP.idPedido " +
                   "JOIN PRODUCTOS PRO ON PP.idProducto = PRO.id " +
                   "WHERE P.Id = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                dataTable.Load(datos.Lector);//Llenamos la DataTable

                total = Convert.ToSingle(dataTable.Rows[0]["Total"]);//Convierto el único resultado que me da esa consulta 

                return total;
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
