using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Mensajes
    {
        public int IdMensaje { get; set; }
        public string ContenidoMensaje { get; set; }
        public DateTime FechaMensaje { get; set; }
        public int IdUsuario { get; set; }
        public int IdChat { get; set; }

        public virtual Chats IdChatNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
