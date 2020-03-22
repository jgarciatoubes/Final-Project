using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Asignaturas
    {
        public Asignaturas()
        {
            Notas = new HashSet<Notas>();
            ProfesorAsignatura = new HashSet<ProfesorAsignatura>();
        }

        public int IdAsignatura { get; set; }
        public string NombreAsignatura { get; set; }

        public virtual ICollection<Notas> Notas { get; set; }
        public virtual ICollection<ProfesorAsignatura> ProfesorAsignatura { get; set; }
    }
}
