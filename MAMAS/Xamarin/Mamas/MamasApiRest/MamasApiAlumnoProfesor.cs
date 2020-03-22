using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {

        public ArrayAlumnoProfesor AlumnoProfesorGet(int idProfesor)
        {
            var result = (ArrayAlumnoProfesor)null;
            var wsMethod = "Get()";
            var uri = $"AlumnoProfesor/{idProfesor}";

            result = CallWebServiceGetCommon<ArrayAlumnoProfesor, AlumnoProfesor>(uri, wsMethod);
            return result;
        }

        public AlumnoProfesor AlumnoProfesorPost(int idAlumno, int idProfesor)
        {
            var result = (AlumnoProfesor)null;
            var wsMethod = "Post()";
            var uri = $"AlumnoProfesor/{idAlumno}/{idProfesor}";

            result = CallWebServicePostCommon<AlumnoProfesor>(uri, wsMethod, null);
            return result;
        }

        public ArrayAlumnoProfesor AlumnoProfesorDelete(int idAlumno)
        {
            var result = (ArrayAlumnoProfesor)null;
            var wsMethod = "Delete()";
            var uri = $"AlumnoProfesor/{idAlumno}";

            result = CallWebServiceDeleteCommon<ArrayAlumnoProfesor>(uri, wsMethod, null);
            return result;
        }




    }
}
