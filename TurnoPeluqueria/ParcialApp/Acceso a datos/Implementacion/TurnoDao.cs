using ParcialApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos.Implementacion
{
    public class TurnoDao : ITurnoDao
    {
        public bool ExisteTurno(string fecha, string hora)
        {
            bool aux = false;
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_CONTAR_TURNOS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@ctd_turnos", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@hora", hora);
                cmd.ExecuteNonQuery();

                int cant = (int)param.Value;

                if (cant > 0)
                {
                    aux = true;
                }
                else
                {
                    aux = false;
                }

            }
            catch
            {

            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
            return aux;
        }

        public List<Servicio> GetServicios()
        {
            List<Servicio> lServicio = new List<Servicio>();
            DataTable tabla = HelperDB.ObtenerInstancia().Consultar("SP_CONSULTAR_SERVICIOS");
            foreach (DataRow fila in tabla.Rows)
            {
                //lo que esta en la clase ingrediente=lo que esta en la tabla del sql
                int Id = int.Parse(fila["id"].ToString());
                string Nombre = fila["nombre"].ToString();
                int Costo = int.Parse(fila["costo"].ToString());
                string EnPromocion = fila["enPromocion"].ToString();

                Servicio s = new Servicio(Id, Nombre, Costo, EnPromocion);

                lServicio.Add(s);
            }
            return lServicio;
        }

        public bool Save(Turno oTurno)
        {
            bool resultado = true;
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            try
            {
                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("INSERTAR_MAESTRO", conexion, t);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@fecha", oTurno.fecha);
                comando.Parameters.AddWithValue("@hora", oTurno.hora);
                comando.Parameters.AddWithValue("@cliente", oTurno.cliente);


                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@id";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);
                comando.ExecuteNonQuery();


                int idTurno = (int)parametro.Value;
                int detallenro = 1;
                foreach (DetalleTurno det in oTurno.lDetalle)
                {
                    if (det.Servicio != null)//en este caso xq el detalle es nulo en la base de datos
                    {
                        SqlCommand cmd = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_turno", idTurno);
                        cmd.Parameters.AddWithValue("@id_servicio", det.Servicio.Id);//no olvidar poner ingrediente.ingredienteid xq no va al sql
                        cmd.Parameters.AddWithValue("@observaciones", det.Observaciones);
                        

                        cmd.ExecuteNonQuery();
                        detallenro++;
                    }
                }
                t.Commit();

            }
            catch
            {
                if (t != null)
                    t.Rollback();
                resultado = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return resultado;
        }
    }
}
