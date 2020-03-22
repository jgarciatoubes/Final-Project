using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class AlumnoProfesor
    {
        public int IdAlumnoProfesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdProfesor { get; set; }

        public virtual Alumnos IdAlumnoNavigation { get; set; }
        public virtual Profesores IdProfesorNavigation { get; set; }
    }
}
