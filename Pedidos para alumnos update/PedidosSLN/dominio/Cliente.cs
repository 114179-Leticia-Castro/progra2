using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int CodigoPostal { get; set; }
        public List<Pedido> Pedidos { get; set; }

        public Cliente()
        {
            Pedidos = new List<Pedido>();
        }

        public Cliente(int id, string nombre, string apellido)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
        }

        public void AgregarPedido(Pedido oPedido)
        {
            Pedidos.Add(oPedido);
        }

        public void QuitarPedido(int numero)
        {
            Pedidos.RemoveAt(numero);
        }

        public override string ToString()
        {
            return Nombre + " " + Apellido; 
        }
    }
}
