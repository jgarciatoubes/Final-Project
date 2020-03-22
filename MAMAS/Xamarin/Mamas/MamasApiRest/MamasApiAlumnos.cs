using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayAlumnos AlumnosGet()
        {
            var result = (ArrayAlumnos)null;
            var wsMethod = "Get()";
            var uri = $"Alumnos";

            result = CallWebServiceGetCommon<ArrayAlumnos, Alumnos>(uri, wsMethod);
            return result;
        }

        public ArrayAlumnos AlumnosGet(int idPadre)
        {
            var result = (ArrayAlumnos)null;
            var wsMethod = "Get()";
            var uri = $"Alumnos/Padre/{idPadre}";

            result = CallWebServiceGetCommon<ArrayAlumnos, Alumnos>(uri, wsMethod);
            return result;
        }

        public Alumnos AlumnoGet(int idAlumno)
        {
            var result = (Alumnos)null;
            var wsMethod = "Get()";
            var uri = $"Alumnos/Alumno/{idAlumno}";

            result = CallWebServiceGetCommon<Alumnos>(uri, wsMethod);
            return result;
        }

        public Alumnos AlumnosPost(string nombreAlumno, string apellidosAlumno, DateTime fechaNacimiento, int idPadre)
        {
            var result = (Alumnos)null;
            var wsMethod = "Post()";
            var uri = $"Alumnos/{nombreAlumno}/{apellidosAlumno}/{fechaNacimiento.ToShortDateString().Replace('/','-')}/{idPadre} ";

            result = CallWebServicePostCommon<Alumnos>(uri, wsMethod, null);
            return result;
        }

        public Alumnos AlumnosDelete(int idAlumno)
        {
            var result = (Alumnos)null;
            var wsMethod = "Delete()";
            var uri = $"Alumnos/{idAlumno}";

            result = CallWebServiceDeleteCommon<Alumnos>(uri, wsMethod);
            return result;
        }

        public Alumnos AlumnosPut(int idAlumno, string nombreAlumno, string apellidosAlumno, DateTime fechaNacimiento, int idPadre)
        {
            var result = (Alumnos)null;
            var wsMethod = "Put()";
            var uri = $"Alumnos/{idAlumno}/{nombreAlumno}/{apellidosAlumno}/{fechaNacimiento.ToShortDateString().Replace('/','-')}/{idPadre}";

            result = CallWebServicePutCommon<Alumnos>(uri, wsMethod, null);
            return result;
        }



    }
}
