using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayProfesores ProfesoresGet()
        {
            var result = (ArrayProfesores)null;
            var wsMethod = "Get()";
            var uri = $"Profesores";

            result = CallWebServiceGetCommon<ArrayProfesores, Profesores>(uri, wsMethod);
            return result;
        }

        public Profesores ProfesoresGet(int idProfesor)
        {
            var result = (Profesores)null;
            var wsMethod = "Get()";
            var uri = $"Profesores/{idProfesor}";

            result = CallWebServiceGetCommon<Profesores>(uri, wsMethod);
            return result;
        }

        public Profesores ProfesoresGet(int idUsuario, string aux)
        {
            var result = (Profesores)null;
            var wsMethod = "Get()";
            var uri = $"Profesores/{idUsuario}/{aux}";

            result = CallWebServiceGetCommon<Profesores>(uri, wsMethod);
            return result;
        }

        public Profesores ProfesoresPost(string nombreProfesor, string apellidosProfesor, int idUsuario)
        {
            var result = (Profesores)null;
            var wsMethod = "Post()";
            var uri = $"Profesores/{nombreProfesor}/{apellidosProfesor}/{idUsuario} ";

            result = CallWebServicePostCommon<Profesores>(uri, wsMethod, null);
            return result;
        }

        public Profesores ProfesoresDelete(int idProfesor)
        {
            var result = (Profesores)null;
            var wsMethod = "Delete()";
            var uri = $"Profesores/{idProfesor}";

            result = CallWebServiceDeleteCommon<Profesores>(uri, wsMethod);
            return result;
        }

        public Profesores ProfesoresPut(int idProfesores, string nombreProfesor, string apellidosProfesor )
        {
            var result = (Profesores)null;
            var wsMethod = "Put()";
            var uri = $"Profesores/{idProfesores}/{nombreProfesor}/{apellidosProfesor}";

            result = CallWebServicePutCommon<Profesores>(uri, wsMethod, null);
            return result;
        }


    }
}
