using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class UsuarioChat
    {
        public int IdUsuarioChat { get; set; }
        public int IdChat { get; set; }
        public int IdUsuario { get; set; }

        public virtual Chats IdChatNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
