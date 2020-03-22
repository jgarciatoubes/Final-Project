using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayEvaluaciones EvaluacionesGet()
        {
            var result = (ArrayEvaluaciones)null;
            var wsMethod = "Get()";
            var uri = $"Evaluaciones";

            result = CallWebServiceGetCommon<ArrayEvaluaciones, Evaluaciones>(uri, wsMethod);
            return result;
        }

        public Evaluaciones EvaluacionesGet(string nombreEvaluacion)
        {
            var result = (Evaluaciones)null;
            var wsMethod = "Get()";
            var uri = $"Evaluaciones/{nombreEvaluacion}";

            result = CallWebServiceGetCommon<Evaluaciones>(uri, wsMethod);
            return result;
        }

        public Evaluaciones EvaluacionesGet(int idEvaluacion)
        {
            var result = (Evaluaciones)null;
            var wsMethod = "Get()";
            var uri = $"Evaluaciones/EvalPorId/{idEvaluacion}";

            result = CallWebServiceGetCommon<Evaluaciones>(uri, wsMethod);
            return result;
        }

        public Evaluaciones EvaluacionesPost(string nombreEvaluacion)
        {
            var result = (Evaluaciones)null;
            var wsMethod = "Post()";
            var uri = $"Evaluaciones/{nombreEvaluacion}";

            result = CallWebServicePostCommon<Evaluaciones>(uri, wsMethod, null);
            return result;
        }

        public Evaluaciones EvaluacionesDelete(string nombreEvaluacion)
        {
            var result = (Evaluaciones)null;
            var wsMethod = "Delete()";
            var uri = $"Evaluaciones/{nombreEvaluacion}";

            result = CallWebServiceDeleteCommon<Evaluaciones>(uri, wsMethod);
            return result;
        }

        public Evaluaciones EvaluacionesPut(int idEvaluacion, string nombreEvaluacion)
        {
            var result = (Evaluaciones)null;
            var wsMethod = "Put()";
            var uri = $"Evaluaciones/{idEvaluacion}/{nombreEvaluacion}";

            result = CallWebServicePutCommon<Evaluaciones>(uri, wsMethod, null);
            return result;
        }
    }
}
