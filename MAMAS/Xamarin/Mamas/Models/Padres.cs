using System;
using System.Collections.Generic;
using Mamas.MamasApiRest;

namespace API_ProyectoPracticas.Models
{
    public class Padres : MamasApiRestResponseBase
    {
        public Padres()
        {
            Alumnos = new HashSet<Alumnos>();
        }

        public int IdPadre { get; set; }
        public string NombrePadre { get; set; }
        public string ApellidosPadre { get; set; }
        public string TelefonoPadre { get; set; }
        public string DireccionPadre { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<Alumnos> Alumnos { get; set; }

        public override string ToString()
        {
            return $"{IdPadre} {NombrePadre} {ApellidosPadre}";
        }
    }
}
