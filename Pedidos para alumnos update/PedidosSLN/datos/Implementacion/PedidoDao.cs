using CarpinteriaApp.datos;
using RecetasSLN.datos.DTOs;
using RecetasSLN.datos.Interfaz;
using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.Implementacion
{
    public class PedidoDao : IPedidoDao
    {
        public bool RegistrarEntrega(int codigo)
        {
            bool resultado = true;
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand();
                t = conexion.BeginTransaction();
                comando.Connection = conexion;
                comando.Transaction = t;
                comando.CommandText = "SP_REGISTRAR_ENTREGA";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@codigo", codigo);
                comando.ExecuteNonQuery();
                t.Commit();

            }

            catch (Exception)
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

        public bool RegistrarBaja(int codigo)
        {
            bool aux = true;
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();
            SqlTransaction transaccion = null;
            try
            {
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("SP_REGISTRAR_BAJA", conexion, transaccion);
                comando.CommandType = CommandType.StoredProcedure;
                Parametro param = new Parametro("@codigo", codigo);
                comando.Parameters.AddWithValue(param.Clave, param.Valor);
                comando.ExecuteNonQuery();
                transaccion.Commit();
            }
            catch
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                    aux = false;
                }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return aux;
        }

        public List<Cliente> GetCliente()
        {
            List<Cliente> lCliente = new List<Cliente>();
            DataTable tabla = HelperDB.ObtenerInstancia().Consultar("SP_CONSULTAR_CLIENTES");
            //mapear un registro de la tabla a un objeto del modelo del dominio

            foreach (DataRow fila in tabla.Rows)
            {
                int Id = int.Parse(fila["id_cliente"].ToString());
                string Nombre = fila["nombre"].ToString();
                string Apellido = fila["apellido"].ToString();
                //int dni = int.Parse(fila["dni"].ToString());
                //int cod_postal = int.Parse(fila["codigopostal"].ToString());
                // bool activo = fila["activo"].ToString().Equals("S");
                Cliente c = new Cliente(Id, Nombre, Apellido);

                lCliente.Add(c);

            }
            return lCliente;
        }

        public List<PedidoDTO> GetPedido(int cliente, DateTime desde, DateTime hasta)
        {
            List<PedidoDTO> lPedido = new List<PedidoDTO>();
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@cliente", cliente));
            lst.Add(new Parametro("@fecha_desde", desde));
            lst.Add(new Parametro("@fecha_hasta", hasta));



            DataTable tabla = HelperDB.ObtenerInstancia().ConsultaSQL("SP_CONSULTAR_PEDIDOS", lst);
            foreach (DataRow fila in tabla.Rows)//para recorrer la tabla, es el mapeo de presupuesto
            {

                PedidoDTO p = new PedidoDTO();

                p.Codigo = int.Parse(fila[0].ToString());
                p.Cliente = int.Parse(fila[3].ToString());//si no sabemos la posicion ponemos el nombre del campo "cliente"
                p.FechaEntrega = DateTime.Parse(fila[1].ToString());
                p.Entregado = fila[4].ToString();
                lPedido.Add(p);
            }
            return lPedido;
        }
    }
}
