using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Chats
    {
        public Chats()
        {
            Mensajes = new HashSet<Mensajes>();
            UsuarioChat = new HashSet<UsuarioChat>();
        }

        public int IdChat { get; set; }
        public int IdTipo { get; set; }
        public string NombreChat { get; set; }

        public virtual TiposChat IdTipoNavigation { get; set; }
        public virtual ICollection<Mensajes> Mensajes { get; set; }
        public virtual ICollection<UsuarioChat> UsuarioChat { get; set; }
    }
}
