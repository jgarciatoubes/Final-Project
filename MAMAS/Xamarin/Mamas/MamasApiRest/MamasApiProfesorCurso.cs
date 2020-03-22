using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ProfesorCurso ProfesorCursoPost(int idProfesor, int idCurso)
        {
            var result = (ProfesorCurso)null;
            var wsMethod = "Post()";
            var uri = $"ProfesorCurso/{idProfesor}/{idCurso}";

            result = CallWebServicePostCommon<ProfesorCurso>(uri, wsMethod, null);
            return result;
        }

        public ArrayProfesorCurso ProfesorCursoDelete(int idProfesor)
        {
            var result = (ArrayProfesorCurso)null;
            var wsMethod = "Delete()";
            var uri = $"ProfesorCurso/{idProfesor}";

            result = CallWebServiceDeleteCommon<ArrayProfesorCurso>(uri, wsMethod);
            return result;
        }




    }
}
