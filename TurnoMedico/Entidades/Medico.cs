using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnoMedico.Entidades
{
    public class Medico
    {
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }


        public Medico(int mat, string nom, string ape, string espe)
        {
            Matricula = mat;
            Nombre = nom;
            Apellido = ape;
            Especialidad = espe;
        }

        public Medico()
        {
            Matricula = 0;
            Nombre = "";
            Apellido = "";
            Especialidad = "";

        }
        public override string ToString()
        {
            return Especialidad;
        }
    }
}
