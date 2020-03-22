using Mamas.MamasApiRest;
using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class ProfesorCurso : MamasApiRestResponseBase
    {
        public int IdProfesorCurso { get; set; }
        public int IdProfesor { get; set; }
        public int IdCurso { get; set; }

        public virtual Cursos IdCursoNavigation { get; set; }
        public virtual Profesores IdProfesorNavigation { get; set; }
    }
}
