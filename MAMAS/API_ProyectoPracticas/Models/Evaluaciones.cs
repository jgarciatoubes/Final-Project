using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Evaluaciones
    {
        public Evaluaciones()
        {
            Notas = new HashSet<Notas>();
        }

        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }

        public virtual ICollection<Notas> Notas { get; set; }
    }
}
