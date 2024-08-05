using RecetasSLN.datos.DTOs;
using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.Servicios.Interfaz
{
    public interface IServicio
    {
        List<Cliente> TraerCliente();
        List<PedidoDTO> TraerPedido(int cliente, DateTime desde, DateTime hasta);
        bool RegistrarBaja(int codigo);
        bool RegistrarPedido(int codigo);
    }
}
