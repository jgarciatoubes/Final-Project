using API_ProyectoPracticas.Models;
using System;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayPadres PadresGet()
        {
            var result = (ArrayPadres)null;
            var wsMethod = "Get()";
            var uri = $"Padres";

            result = CallWebServiceGetCommon<ArrayPadres, Padres>(uri, wsMethod);
            return result;
        }

        public Padres PadresGet(int idPadre)
        {
            var result = (Padres)null;
            var wsMethod = "Get()";
            var uri = $"Padres/PadrePorIdPadre/{idPadre}";

            result = CallWebServiceGetCommon<Padres>(uri, wsMethod);
            return result;
        }

        public Padres PadrePorIdUsuGet(int idUsuario)
        {
            var result = (Padres)null;
            var wsMethod = "Get()";
            var uri = $"Padres/PadrePorIdUsu/{idUsuario}";

            result = CallWebServiceGetCommon<Padres>(uri, wsMethod);
            return result;
        }

        public Padres PadresPost(int idUsuario, string nombrePadre, string apellidosPadre, string direccionPadre, string telefonoPadre)
        {
            var result = (Padres)null;
            var wsMethod = "Post()";
            var uri = $"Padres/{idUsuario}/{nombrePadre}/{apellidosPadre}/{direccionPadre}/{telefonoPadre}";

            result = CallWebServicePostCommon<Padres>(uri, wsMethod, null);
            return result;
        }

        public Padres PadresDelete(int idPadre)
        {
            var result = (Padres)null;
            var wsMethod = "Delete()";
            var uri = $"Padres/{idPadre}";

            result = CallWebServiceDeleteCommon<Padres>(uri, wsMethod);
            return result;
        }

        public Padres PadresPut(int idPadre, string nombrePadre, string apellidosPadre, string direccionPadre, string telefonoPadre)
        {
            var result = (Padres)null;
            var wsMethod = "Put()";
            var uri = $"Padres/{idPadre}/{nombrePadre}/{apellidosPadre}/{direccionPadre}/{telefonoPadre}";

            result = CallWebServicePutCommon<Padres>(uri, wsMethod, null);
            return result;
        }


    }
}
