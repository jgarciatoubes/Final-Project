using Mamas.MamasApiRest;
using API_ProyectoPracticas.Models;
using System;
using System.Linq;

namespace TestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
                //Ruta = new Uri("https://huawei-david:8443/api"),
            };
            DateTime now = DateTime.Now;

            var resp = mamas.NotasPost("8", 1, 2, 1, 1);

            /*var resp = mamas.UsuariosPost("jgarcia", "jgarcia", 2);
            var resp2 = mamas.ProfesoresPost("Jorge", "García Toubes", resp.IdUsuario);
            var resp3 = mamas.ProfesorAsignaturaPost(resp2.IdProfesor, 1);
            var resp4 = mamas.ProfesorCursoPost(resp2.IdProfesor, 5);
            var resp5 = mamas.ProfesorAsignaturaDelete(resp2.IdProfesor);
            var resp6 = mamas.ProfesorCursoDelete(resp2.IdProfesor);
            var resp7 = mamas.ProfesoresDelete(resp2.IdProfesor);
            var resp8 = mamas.UsuariosDelete(resp.IdUsuario);*/
//          Console.ReadKey();
        }
    }
}
