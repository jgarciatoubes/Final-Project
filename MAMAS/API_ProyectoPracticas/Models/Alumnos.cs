using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Alumnos
    {
        public Alumnos()
        {
            AlumnoProfesor = new HashSet<AlumnoProfesor>();
            Notas = new HashSet<Notas>();
        }

        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidosAlumnos { get; set; }
        public DateTime FechaNacimientoAlumno { get; set; }
        public int IdPadre { get; set; }

        public virtual Padres IdPadreNavigation { get; set; }
        public virtual ICollection<AlumnoProfesor> AlumnoProfesor { get; set; }
        public virtual ICollection<Notas> Notas { get; set; }
    }
}
