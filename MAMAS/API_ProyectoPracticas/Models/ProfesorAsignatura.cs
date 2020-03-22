using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class ProfesorAsignatura
    {
        public int IdProfesorAsignatura { get; set; }
        public int IdProfesor { get; set; }
        public int IdAsignatura { get; set; }

        public virtual Asignaturas IdAsignaturaNavigation { get; set; }
        public virtual Profesores IdProfesorNavigation { get; set; }
    }
}
