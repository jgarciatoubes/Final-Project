using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class TiposChat
    {
        public TiposChat()
        {
            Chats = new HashSet<Chats>();
        }

        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }

        public virtual ICollection<Chats> Chats { get; set; }
    }
}
