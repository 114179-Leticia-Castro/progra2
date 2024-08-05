using RecetasSLN.datos.DTOs;
using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.Interfaz
{
    public interface IPedidoDao
    {
        List<Cliente> GetCliente();
        List<PedidoDTO> GetPedido(int cliente, DateTime desde, DateTime hasta);
        bool RegistrarEntrega (int codigo);
        bool RegistrarBaja (int codigo);
    }
}
