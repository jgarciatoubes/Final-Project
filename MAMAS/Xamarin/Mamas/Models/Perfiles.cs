using System;
using System.Collections.Generic;

namespace API_ProyectoPracticas.Models
{
    public class Perfiles
    {
        public Perfiles()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; }

        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
