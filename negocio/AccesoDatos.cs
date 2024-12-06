using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//Poruqe voy a usar el sql aca

namespace negocio
{
    public  class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        //Para leer el lector desde el exterior
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        //creo la conexion
        public AccesoDatos()
        {
            //Centralizamos la cadena de conexion a la BD
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=FINALRESTOPROG; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearProcedimiento(string sp)//Para ejecutar StoresProcedures
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                //Esto ejecuta la query
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cerrarConexion();
            }
        }
        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                //Esto ejecuta la query
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cerrarConexion();
            }
        }

        public void setearParametro(string nombre, object valor)//Para validar las consultas, Recibe un string y un object
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void cerrarConexion()
        {
            if (lector != null)//Si realice una lectura
                lector.Close();
            conexion.Close();
        }

    }
}
