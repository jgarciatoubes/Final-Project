using System;
using Mamas.MamasApiRest;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Usuarios : MamasApiRestResponseBase
    {
        public Usuarios()
        {
            Mensajes = new HashSet<Mensajes>();
            Padres = new HashSet<Padres>();
            Profesores = new HashSet<Profesores>();
            UsuarioChat = new HashSet<UsuarioChat>();
        }

        public int IdUsuario { get; set; }
        public string UsernameUsuario { get; set; }
        public string PassUsuario { get; set; }
        public int IdPerfil { get; set; }

        public virtual Perfiles IdPerfilNavigation { get; set; }
        public virtual ICollection<Mensajes> Mensajes { get; set; }
        public virtual ICollection<Padres> Padres { get; set; }
        public virtual ICollection<Profesores> Profesores { get; set; }
        public virtual ICollection<UsuarioChat> UsuarioChat { get; set; }
    }
}
