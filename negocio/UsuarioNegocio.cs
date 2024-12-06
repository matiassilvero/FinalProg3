using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Configuration;
using dominio;
using negocio;
using System.Net;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar(bool soloMeseros, string id = "")//Este string permite que LISTAR funcione de 2 maneras
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;//Devuelve tipo object

            try
            {
                conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = soloMeseros ? "SELECT id, nombre, apellido, dni, pass, fechaNacimiento, urlImagen, tipoUsuario, activo FROM USUARIOS WHERE TIPOUSUARIO = 2"
                                                  : "SELECT id, nombre, apellido, dni, pass, fechaNacimiento, urlImagen, tipoUsuario, activo FROM USUARIOS";
                if (id != "" && soloMeseros)
                {
                    comando.CommandText += " AND id = " + id;
                }
                if (id != "" && !soloMeseros)
                {
                    comando.CommandText += " WHERE id = " + id;
                }

                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)lector["id"];
                    aux.Nombre = (string)lector["nombre"];
                    aux.Apellido = (string)lector["apellido"];
                    aux.Dni = (string)lector["dni"];
                    aux.Pass = (string)lector["pass"];
                    aux.FechaNacimiento = (DateTime)lector["fechaNacimiento"];
                    aux.UrlImagen = (lector["urlImagen"] is DBNull || (string)lector["urlImagen"] == "") ? "default.jpg" : (string)lector["urlImagen"];
                    aux.TipoUsuario = (TipoUsuario)(int)lector["tipoUsuario"];
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

        public string obtenerApellido(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT apellido FROM USUARIOS WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read()) 
                {
                    return datos.Lector["apellido"].ToString();
                }
                else
                {
                    return null; 
                }
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
        public bool login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select id, nombre, apellido, dni, pass, fechaNacimiento, urlImagen, tipoUsuario, activo FROM USUARIOS Where dni = @dni AND pass = @pass");
                datos.setearParametro("@dni", usuario.Dni);
                datos.setearParametro("@pass", usuario.Pass);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Nombre = (string)datos.Lector["nombre"];
                    usuario.Apellido = (string)datos.Lector["apellido"];
                    usuario.Dni = (string)datos.Lector["dni"];
                    usuario.Pass = (string)datos.Lector["pass"];
                    usuario.FechaNacimiento = (DateTime)datos.Lector["fechaNacimiento"];
                    if (!(datos.Lector["urlImagen"] is DBNull))
                        usuario.UrlImagen = (string)datos.Lector["urlImagen"];
                    usuario.TipoUsuario = (TipoUsuario)(int)datos.Lector["tipoUsuario"];
                    usuario.Activo = bool.Parse(datos.Lector["activo"].ToString());

                    return true;
                }
                return false;
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

        public int agregar(Usuario usuario)//Agregar y que devuelva el int del Id
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO USUARIOS (nombre, apellido, dni, pass, fechaNacimiento, urlImagen, tipoUsuario, activo) " +
                    "output inserted.id VALUES(@nombre, @apellido, @dni, @pass, @fechaNacimiento, @urlImagen, @tipoUsuario, @activo)");
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@dni", usuario.Dni);
                datos.setearParametro("@pass", usuario.Pass);
                datos.setearParametro("@fechaNacimiento", usuario.FechaNacimiento);
                //datos.setearParametro("@urlImagen", usuario.UrlImagen);
                datos.setearParametro("@urlImagen", (object)usuario.UrlImagen ?? DBNull.Value);//Por si pasamos NULL
                datos.setearParametro("@tipoUsuario", usuario.TipoUsuario);
                datos.setearParametro("@activo", true);
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

        public bool existeDni(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select dni FROM USUARIOS");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    if(dni == datos.Lector["dni"].ToString())
                    {
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
        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE USUARIOS set nombre = @nombre, apellido = @apellido, dni = @dni, pass = @pass, fechaNacimiento = @fechaNacimiento, urlImagen = @urlImagen, tipoUsuario = @tipoUsuario, activo = @activo Where id = @id");
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@dni", usuario.Dni);
                datos.setearParametro("@pass", usuario.Pass);
                datos.setearParametro("@fechaNacimiento", usuario.FechaNacimiento);
                //datos.setearParametro("@urlImagen", usuario.UrlImagen);
                datos.setearParametro("@urlImagen", (object)usuario.UrlImagen ?? DBNull.Value);//Por si pasamos NULL
                datos.setearParametro("@tipoUsuario", (int)usuario.TipoUsuario);
                datos.setearParametro("@activo", true);
                datos.setearParametro("@id", usuario.Id);
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

        public void modificarImagen(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET urlImagen = @urlImagen WHERE id = @id");
                //datos.setearParametro("@urlImagen", usuario.UrlImagen);
                datos.setearParametro("@urlImagen", (object)usuario.UrlImagen ?? DBNull.Value);//Por si pasamos NULL
                datos.setearParametro("@id", usuario.Id);
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
                datos.setearConsulta("DELETE FROM USUARIOS Where id = @id");
                datos.setearParametro("@id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void activarDesactivar(int Id, bool Activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE USUARIOS SET activo = @activo Where id = @id");
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
