using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Cursos
    {
        public Cursos()
        {
            Notas = new HashSet<Notas>();
            ProfesorCurso = new HashSet<ProfesorCurso>();
        }

        public int IdCurso { get; set; }
        public int NumeroCurso { get; set; }
        public string LetraCurso { get; set; }

        public virtual ICollection<Notas> Notas { get; set; }
        public virtual ICollection<ProfesorCurso> ProfesorCurso { get; set; }
    }
}
