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
    public class MesaNegocio
    {
        public List<Mesa> listar(bool soloMesasLibres, string id = "")
        {
            List<Mesa> lista = new List<Mesa>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = soloMesasLibres ? "SELECT id, capacidad, disponibilidad, activo FROM MESAS WHERE DiSPONIBILIDAD = 1"
                                                      : "SELECT id, capacidad, disponibilidad, activo FROM MESAS";
                if (id != "" && soloMesasLibres)
                {
                    comando.CommandText += " AND id = " + id;
                }
                if (id != "" && !soloMesasLibres)
                {
                    comando.CommandText += " WHERE id = " + id;
                }

                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Mesa aux = new Mesa();
                    aux.Id = (int)lector["id"];
                    aux.Capacidad = (int)lector["capacidad"];
                    aux.Disponibilidad= (Disponibilidad)(int)lector["disponibilidad"];
                    aux.Activo = bool.Parse(lector["activo"].ToString());

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

        //public List<Mesa> listarMesasDisponibles()
        //{
        //    List<Mesa> lista = new List<Mesa>();
        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand comando = new SqlCommand();
        //    SqlDataReader lector;

        //    try
        //    {
        //        conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
        //        comando.CommandType = System.Data.CommandType.Text;
        //        comando.CommandText = "SELECT id, capacidad, disponibilidad, activo FROM MESAS WHERE disponibilidad = 1";
        //        comando.Connection = conexion;
        //        conexion.Open();
        //        lector = comando.ExecuteReader();

        //        while (lector.Read())
        //        {
        //            Mesa aux = new Mesa();
        //            aux.Id = (int)lector["id"];
        //            aux.Capacidad = (int)lector["capacidad"];
        //            aux.Disponibilidad = (Disponibilidad)(int)lector["disponibilidad"];
        //            aux.Activo = bool.Parse(lector["activo"].ToString());

        //            lista.Add(aux);
        //        }

        //        conexion.Close();
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void agregar(Mesa nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MESAS (capacidad, disponibilidad, activo)Values(@capacidad, @disponibilidad, 1)");
                datos.setearParametro("@capacidad", nuevo.Capacidad);
                datos.setearParametro("@disponibilidad", nuevo.Disponibilidad);
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

        public void modificar(Mesa mesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MESAS SET capacidad = @capacidad, disponibilidad = @disponibilidad Where id = @id");
                datos.setearParametro("@capacidad", mesa.Capacidad);
                datos.setearParametro("@disponibilidad",mesa.Disponibilidad);
                datos.setearParametro("@id", mesa.Id);
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

        public void modificarDisponibilidad(int id, int disponibilidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MESAS SET disponibilidad = @disponibilidad Where id = @id");
                datos.setearParametro("@disponibilidad", disponibilidad);
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

        public void eliminar(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM MESAS Where id = @id");
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
                datos.setearConsulta("UPDATE MESAS set activo = @activo Where id = @id");
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
