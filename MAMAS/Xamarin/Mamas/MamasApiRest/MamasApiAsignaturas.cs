using RestSharp;
using API_ProyectoPracticas.Models;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayAsignaturas AsignaturasGet()
        {
            var result = (ArrayAsignaturas)null;
            var wsMethod = "Get()";
            var uri = $"Asignaturas";

            result = CallWebServiceGetCommon<ArrayAsignaturas, Asignaturas>(uri, wsMethod);
            return result;
        }

        public Asignaturas AsignaturasGet(string nombreAsignatura)
        {
            var result = (Asignaturas)null;
            var wsMethod = "Get()";
            var uri = "Asignaturas/" + nombreAsignatura;

            result = CallWebServiceGetCommon<Asignaturas>(uri, wsMethod);
            return result;
        }

        public Asignaturas AsignaturasGet(int idAsignatura)
        {
            var result = (Asignaturas)null;
            var wsMethod = "Get()";
            var uri = "Asignaturas/AsigPorId/" + idAsignatura;

            result = CallWebServiceGetCommon<Asignaturas>(uri, wsMethod);
            return result;
        }

        public Asignaturas AsignaturasPost(string nombreAsignatura)
        {
            var result = (Asignaturas)null;
            var wsMethod = "Post()";
            var uri = "Asignaturas/" + nombreAsignatura;

            result = CallWebServicePostCommon<Asignaturas>(uri, wsMethod, null);
            return result;
        }

        public Asignaturas AsignaturasDelete(string nombreAsignatura)
        {
            var result = (Asignaturas)null;
            var wsMethod = "Delete()";
            var uri = "Asignaturas/" + nombreAsignatura;

            result = CallWebServiceDeleteCommon<Asignaturas>(uri, wsMethod);
            return result;
        }

        public Asignaturas AsignaturasPut(int idAsignatura, string nombreAsignatura)
        {
            var result = (Asignaturas)null;
            var wsMethod = "Put()";
            var uri = "Asignaturas/" + idAsignatura + "/" + nombreAsignatura;

            result = CallWebServicePutCommon<Asignaturas>(uri, wsMethod, null);
            return result;
        }
    }
}
