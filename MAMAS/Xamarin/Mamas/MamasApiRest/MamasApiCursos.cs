using RestSharp;
using API_ProyectoPracticas.Models;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayCursos CursosGet()
        {
            var result = (ArrayCursos)null;
            var wsMethod = "Get()";
            var uri = $"Cursos";

            result = CallWebServiceGetCommon<ArrayCursos, Cursos>(uri, wsMethod);
            return result;
        }

        public Cursos CursosGet(int numeroCurso, string letraCurso)
        {
            var result = (Cursos)null;
            var wsMethod = "Get()";
            var uri = $"Cursos/{numeroCurso}/{letraCurso}";

            result = CallWebServiceGetCommon<Cursos>(uri, wsMethod);
            return result;
        }

        public Cursos CursosGet(int idcurso)
        {
            var result = (Cursos)null;
            var wsMethod = "Get()";
            var uri = $"Cursos/{idcurso}";

            result = CallWebServiceGetCommon<Cursos>(uri, wsMethod);
            return result;
        }

        public Cursos CursosPost(int numeroCurso, string letraCurso)
        {
            var result = (Cursos)null;
            var wsMethod = "Post()";
            var uri = $"Cursos/{numeroCurso}/{letraCurso}";

            result = CallWebServicePostCommon<Cursos>(uri, wsMethod, null);
            return result;
        }

        public Cursos CursosDelete(int numeroCurso, string letraCurso)
        {
            var result = (Cursos)null;
            var wsMethod = "Delete()";
            var uri = $"Cursos/{numeroCurso}/{letraCurso}";

            result = CallWebServiceDeleteCommon<Cursos>(uri, wsMethod);
            return result;
        }

        public Cursos CursosPut(int idCurso, int numeroCurso, string letraCurso)
        {
            var result = (Cursos)null;
            var wsMethod = "Put()";
            var uri = $"Cursos/{idCurso}/{numeroCurso}/{letraCurso}";

            result = CallWebServicePutCommon<Cursos>(uri, wsMethod, null);
            return result;
        }
    }
}
