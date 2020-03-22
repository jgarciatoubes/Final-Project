using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Profesores
    {
        public Profesores()
        {
            AlumnoProfesor = new HashSet<AlumnoProfesor>();
            ProfesorAsignatura = new HashSet<ProfesorAsignatura>();
            ProfesorCurso = new HashSet<ProfesorCurso>();
        }

        public int IdProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public string ApellidosProfesor { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<AlumnoProfesor> AlumnoProfesor { get; set; }
        public virtual ICollection<ProfesorAsignatura> ProfesorAsignatura { get; set; }
        public virtual ICollection<ProfesorCurso> ProfesorCurso { get; set; }
    }
}
