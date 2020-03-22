using Mamas.MamasApiRest;
using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Notas : MamasApiRestResponseBase
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

        public override string ToString()
        {
            MamasApi mamas;
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            var respAlumno = mamas.AlumnoGet(IdAlumno);
            Alumnos alumno = new Alumnos();
            alumno.IdAlumno = IdAlumno;
            alumno.NombreAlumno = respAlumno.NombreAlumno;
            alumno.ApellidosAlumnos = respAlumno.ApellidosAlumnos;
            alumno.IdPadre = respAlumno.IdPadre;

            var respAsignatura = mamas.AsignaturasGet(IdAsignatura);
            Asignaturas asignatura = new Asignaturas();
            asignatura.IdAsignatura = respAsignatura.IdAsignatura;
            asignatura.NombreAsignatura = respAsignatura.NombreAsignatura;

            var respCurso = mamas.CursosGet(IdCurso);
            Cursos curso = new Cursos();
            curso.IdCurso = respCurso.IdCurso;
            curso.NumeroCurso = respCurso.NumeroCurso;
            curso.LetraCurso = respCurso.LetraCurso;

            var respEvaluacion = mamas.EvaluacionesGet(IdEvaluacion);
            Evaluaciones evaluacion = new Evaluaciones();
            evaluacion.IdEvaluacion = respEvaluacion.IdEvaluacion;
            evaluacion.NombreEvaluacion = respEvaluacion.NombreEvaluacion;

            return $"{curso.NumeroCurso}{curso.LetraCurso}, Evaluacion {evaluacion.NombreEvaluacion},  {asignatura.NombreAsignatura}{Environment.NewLine}NOTA: {Nota}";
        }
    }
}
