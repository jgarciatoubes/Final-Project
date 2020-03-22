using API_ProyectoPracticas.Models;

namespace Mamas.MamasApiRest
{
    public partial class MamasApi
    {
        public ArrayUsuarios UsuariosGet()
        {
            var result = (ArrayUsuarios)null;
            var wsMethod = "Get()";
            var uri = $"Usuarios";

            result = CallWebServiceGetCommon<ArrayUsuarios, Usuarios>(uri, wsMethod);
            return result;
        }

        public Usuarios UsuariosGet(string nombreUsuario, string passUsuario)
        {
            var result = (Usuarios)null;
            var wsMethod = "Get()";
            var uri = $"Usuarios/{nombreUsuario}/{passUsuario}";

            result = CallWebServiceGetCommon<Usuarios>(uri, wsMethod);
            return result;
        }

        public Usuarios UsuariosGet(string nombreUsuario)
        {
            var result = (Usuarios)null;
            var wsMethod = "Get()";
            var uri = $"Usuarios/{nombreUsuario}";

            result = CallWebServiceGetCommon<Usuarios>(uri, wsMethod);
            return result;
        }

        public Usuarios UsuariosGetMayorID()
        {
            var result = (Usuarios)null;
            var wsMethod = "Get()";
            var uri = $"Usuarios/MayorID";

            result = CallWebServiceGetCommon<Usuarios>(uri, wsMethod);
            return result;
        }

        public Usuarios UsuariosPost(string nombreUsuario, string passUsuario, int idPerfil)
        {
            var result = (Usuarios)null;
            var wsMethod = "Post()";
            var uri = $"Usuarios/{nombreUsuario}/{passUsuario}/{idPerfil}";

            result = CallWebServicePostCommon<Usuarios>(uri, wsMethod, null);
            return result;
        }

        /*public Usuarios UsuariosDelete(string nombreUsuario)
        {
            var result = (Usuarios)null;
            var wsMethod = "Delete()";
            var uri = $"Usuarios/{nombreUsuario}";

            result = CallWebServiceDeleteCommon<Usuarios>(uri, wsMethod);
            return result;
        }*/

        public Usuarios UsuariosDelete(int idUsuario)
        {
            var result = (Usuarios)null;
            var wsMethod = "Delete()";
            var uri = $"Usuarios/{idUsuario}";

            result = CallWebServiceDeleteCommon<Usuarios>(uri, wsMethod);
            return result;
        }

        public Usuarios UsuariosPut(int idUsuario, string nombreUsuario, string passUsuario, int idPerfil)
        {
            var result = (Usuarios)null;
            var wsMethod = "Put()";
            var uri = $"Usuarios/{idUsuario}/{nombreUsuario}/{passUsuario}/{idPerfil}";

            result = CallWebServicePutCommon<Usuarios>(uri, wsMethod, null);
            return result;
        }

        public Usuarios UsuariosPut(int idUsuario, string passUsuario, int idPerfil)
        {
            var result = (Usuarios)null;
            var wsMethod = "Put()";
            var uri = $"Usuarios/{idUsuario}/{passUsuario}/{idPerfil}";

            result = CallWebServicePutCommon<Usuarios>(uri, wsMethod, null);
            return result;
        }
    }
}
