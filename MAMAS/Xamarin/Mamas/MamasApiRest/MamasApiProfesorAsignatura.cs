using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ProfesorAsignatura ProfesorAsignaturaPost(int idProfesor, int idAsignatura)
        {
            var result = (ProfesorAsignatura)null;
            var wsMethod = "Post()";
            var uri = $"ProfesorAsignatura/{idProfesor}/{idAsignatura}";

            result = CallWebServicePostCommon<ProfesorAsignatura>(uri, wsMethod, null);
            return result;
        }

        public ArrayProfesorAsignatura ProfesorAsignaturaDelete(int idProfesor)
        {
            var result = (ArrayProfesorAsignatura)null;
            var wsMethod = "Delete()";
            var uri = $"ProfesorAsignatura/{idProfesor}";

            result = CallWebServiceDeleteCommon<ArrayProfesorAsignatura>(uri, wsMethod);
            return result;
        }




    }
}
