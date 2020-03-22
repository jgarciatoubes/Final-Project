using Mamas.MamasApiRest;
using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class AlumnoProfesor : MamasApiRestResponseBase
    {
        public int IdAlumnoProfesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdProfesor { get; set; }

        public virtual Alumnos IdAlumnoNavigation { get; set; }
        public virtual Profesores IdProfesorNavigation { get; set; }

        public override string ToString()
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            var resp = mamas.AlumnoGet(IdAlumno);
            Alumnos alumno = new Alumnos();
            alumno.IdAlumno = IdAlumno;
            alumno.NombreAlumno = resp.NombreAlumno;
            alumno.ApellidosAlumnos = resp.ApellidosAlumnos;
            alumno.IdPadre = resp.IdPadre;
            return alumno.ToString();
        }
    }
}
