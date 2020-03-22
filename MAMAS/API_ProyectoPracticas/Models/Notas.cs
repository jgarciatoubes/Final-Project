using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Notas
    {
        public int IdNota { get; set; }
        public string Nota { get; set; }
        public int IdAsignatura { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdCurso { get; set; }
        public int IdAlumno { get; set; }

        public virtual Alumnos IdAlumnoNavigation { get; set; }
        public virtual Asignaturas IdAsignaturaNavigation { get; set; }
        public virtual Cursos IdCursoNavigation { get; set; }
        public virtual Evaluaciones IdEvaluacionNavigation { get; set; }
    }
}
