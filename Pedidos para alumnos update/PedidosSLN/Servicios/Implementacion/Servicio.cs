using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecetasSLN.datos.DTOs;
using RecetasSLN.datos.Implementacion;
using RecetasSLN.datos.Interfaz;
using RecetasSLN.dominio;
using RecetasSLN.Servicios.Interfaz;

namespace RecetasSLN.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private IPedidoDao dao;

        public Servicio()
        {
            dao = new PedidoDao();
        }

        public bool RegistrarBaja(int codigo)
        {
           return dao.RegistrarBaja(codigo);
        }

        public bool RegistrarPedido(int codigo)
        {
            return dao.RegistrarEntrega(codigo);
        }

        public List<Cliente> TraerCliente()
        {
            return dao.GetCliente();
        }

        public List<PedidoDTO> TraerPedido(int cliente, DateTime desde, DateTime hasta)
        {
            return dao.GetPedido(cliente, desde, hasta);
        }
    }
}
