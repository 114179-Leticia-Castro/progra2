using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnoMedico.Datos.Helper;
using TurnoMedico.Datos.Interfaz;
using TurnoMedico.Entidades;

namespace TurnoMedico.Datos.Implementacion
{
    internal class TurnoDao : ITurnoDao
    {


        public bool CrearTurno(Turno oTurno)
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
                comando.Parameters.AddWithValue("@paciente", oTurno.Paciente);
                


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
                    if (det.Medico != null)//en este caso xq el detalle es nulo en la base de datos
                    {
                        SqlCommand cmd = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_turno", idTurno);
                        cmd.Parameters.AddWithValue("@matricula", det.Medico.Matricula);//no olvidar poner ingrediente.ingredienteid xq no va al sql
                        cmd.Parameters.AddWithValue("@motivo", det.MotivoConsulta);
                        cmd.Parameters.AddWithValue("@fecha", det.Fecha);
                        cmd.Parameters.AddWithValue("@hora", det.Hora);

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

        public bool ExistsTurno(string fecha,string hora, int matricula)
        {
            bool aux = false;
            SqlConnection cnn = HelperDB.ObtenerInstancia().ObtenerConexion();

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("SP_CONTAR_TURNOS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@ctd_turnos", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@hora", hora);
                cmd.Parameters.AddWithValue("@matricula ", matricula);
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
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
            return aux;
        }

        public List<Medico> ObtenerMedico()
        {
            List<Medico> lMedico = new List<Medico>();
            DataTable tabla = HelperDB.ObtenerInstancia().Consultar("SP_CONSULTAR_MEDICOS");
            foreach (DataRow fila in tabla.Rows)
            {
                //lo que esta en la clase ingrediente=lo que esta en la tabla del sql
                int matricula = int.Parse(fila["matricula"].ToString());
                string nombre = fila["nombre"].ToString();
                string apellido = fila["apellido"].ToString();
                string especialidad = fila["especialidad"].ToString();

                Medico m = new Medico(matricula, nombre, apellido, especialidad);

                lMedico.Add(m);
            }
            return lMedico;
        }
    }
}
