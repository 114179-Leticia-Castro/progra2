using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.DTOs
{
    public class ClienteDTO
    {
        public ClienteDTO(int id, string nombrecompleto)
        {
            Id = id;
            NombreCompleto = nombrecompleto;
        }

        public int Id { get; set; }
        public string NombreCompleto { get; set; }

        public override string ToString()
        {
            return NombreCompleto;
        }
    }

   
}
