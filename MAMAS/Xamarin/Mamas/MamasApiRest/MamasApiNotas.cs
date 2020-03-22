using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayNotas NotasGet(int idAlumno)
        {
            var result = (ArrayNotas)null;
            var wsMethod = "Get()";
            var uri = $"Notas/{idAlumno}";

            result = CallWebServiceGetCommon<ArrayNotas, Notas>(uri, wsMethod);
            return result;
        }

        public Notas NotasGet(int idAsignatura, int idEvaluacion, int idCurso, int idAlumno)
        {
            var result = (Notas)null;
            var wsMethod = "Get()";
            var uri = $"Notas/{idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}";

            result = CallWebServiceGetCommon<Notas>(uri, wsMethod);
            return result;
        }

        public Notas NotasPost(string notaValor, int idAsignatura, int idEvaluacion, int idCurso, int idAlumno)
        {
            var result = (Notas)null;
            var wsMethod = "Post()";
            var uri = $"Notas/{notaValor}/{idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}";

            result = CallWebServicePostCommon<Notas>(uri, wsMethod, null);
            return result;
        }

        public Notas NotasDelete(int idNota)
        {
            var result = (Notas)null;
            var wsMethod = "Delete()";
            var uri = $"Notas/{idNota}";

            result = CallWebServiceDeleteCommon<Notas>(uri, wsMethod);
            return result;
        }
        public Notas NotasPut(int idNota, string notaValor, int idAsignatura, int idEvaluacion, int idCurso, int idAlumno)
        {
            var result = (Notas)null;
            var wsMethod = "Put()";
            var uri = $"Notas/{idNota}/{notaValor}/{idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}";

            result = CallWebServicePutCommon<Notas>(uri, wsMethod, null);
            return result;
        }

    }
}
