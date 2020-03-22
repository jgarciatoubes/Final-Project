using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_ProyectoPracticas.Models;

namespace API_ProyectoPracticas.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        /// <summary>
        /// Devuelve los datos de todos los alumnos
        /// </summary> 
        /// <returns>El JSON de todos los alumnos</returns>
        /// <response code="200">Si existen alumnos</response>
        /// <response code="404">Si no existen alumnos</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();

            if (!context.Alumnos.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay alumnos en la base de datos",
                    Detail = $"No hay alumnos en la tabla Alumnos de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Alumnos);
        }

        /// <summary>
        /// Devuelve los alumnos de un mismo padre
        /// </summary> 
        /// <param name="idPadre"> El identificador del padre</param>
        /// <returns>El JSON de los alumnos</returns>
        /// <response code="200">Si el padre existe y tiene alumnos hijos</response>
        /// <response code="404">Si el padre no existe</response>
        /// <response code="400">Si no tiene alumnos hijos</response>
        [HttpGet("Padre/{idPadre}")]
        public IActionResult GetPorPadre(int idPadre)
        {
            MAMASContext context = new MAMASContext();
            var padre = context.Padres.SingleOrDefault(p => p.IdPadre == idPadre);
            if (padre == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Id de padre inexistente",
                    Detail = $"No existe el padre con identificador: {idPadre}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
            {
                var alumnos = context.Alumnos.Where(p => p.IdPadre == idPadre);
                if (!alumnos.Any())
                {
                    var details = new ProblemDetails()
                    {
                        Title = "No hay alumnos bajo ese id de padre",
                        Detail = $"No hay alumnos bajo el id de padre: {idPadre}",
                        Status = 400
                    };
                    return new ObjectResult(details)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = 400,
                    };
                }
                else
                    return Ok(alumnos);
            }
        }

        /// <summary>
        /// Busca un Alumno
        /// </summary>       
        /// <param name="idAlumno"> El id del alumno</param>
        /// <param name="aux"> Parámetro auxiliar</param>
        /// <returns>El JSON del alumno</returns>
        /// <response code="200">Si el alumno existe</response>
        /// <response code="404">Si el alumno no existe</response>
        [HttpGet("Alumno/{idAlumno}")]
        public IActionResult GetAlumno(int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            var alumno = context.Alumnos.SingleOrDefault(p => p.IdAlumno == idAlumno);
            if (alumno == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el alumno",
                    Detail = $"No existe el alumno de id: {idAlumno}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
            {
                return Ok(alumno);
            }
        }

        /// <summary>
        /// Crea un alumno
        /// </summary> 
        /// <param name="nombreAlumno"> El nombre del alumno</param>
        /// <param name="apellidosAlumno"> Los apellidos del alumno</param>
        /// <param name="fechaNacimiento"> La fecha de nacimiento del alumno</param>
        /// <param name="idPadre"> El identificador del padre del alumno</param>
        /// <returns>El JSON del alumno</returns>
        /// <response code="201">Si el alumno ha sido creado</response>
        [HttpPost("{nombreAlumno}/{apellidosAlumno}/{fechaNacimiento}/{idPadre}")]
        public IActionResult Post(string nombreAlumno, string apellidosAlumno, DateTime fechaNacimiento, int idPadre)
        {
            MAMASContext context = new MAMASContext();

            Alumnos alumno = new Alumnos();
            alumno.NombreAlumno = nombreAlumno;
            alumno.ApellidosAlumnos = apellidosAlumno;
            alumno.FechaNacimientoAlumno = fechaNacimiento;
            alumno.IdPadre = idPadre;
            context.Alumnos.Add(alumno);
            context.SaveChanges();
            return CreatedAtRoute("", new
            {
                nombreAlumno = alumno.NombreAlumno,
                apellidosAlumno = alumno.ApellidosAlumnos,
                fechaNacimiento = alumno.FechaNacimientoAlumno,
                idPadre = alumno.IdPadre
            }, alumno);
        }


        /// <summary>
        /// Borra un alumno
        /// </summary>
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <returns>El JSON del alumno borrado</returns>
        /// <response code="200">Si el item ha sido borrado</response>
        /// <response code="404">Si el item no existe</response>
        [HttpDelete("{idAlumno}")]
        public IActionResult Delete(int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            var alumno = context.Alumnos.SingleOrDefault(p => p.IdAlumno == idAlumno);
            if (alumno == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el alumno",
                    Detail = $"No existe el alumno con identificador: {idAlumno}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
            {
                context.Alumnos.Remove(alumno);
                context.SaveChanges();
                return Ok(alumno);
            }

        }

        /// <summary>
        /// Modifica un alumno
        /// </summary> 
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <param name="nombreAlumno"> El nombre del alumno</param>
        /// <param name="apellidosAlumno"> Los apellidos del alumno</param>
        /// <param name="fechaNacimiento"> La fecha de nacimiento del alumno</param>
        /// <param name="idPadre"> El identificador del padre del alumno</param>
        /// <returns>El JSON del alumno</returns>
        /// <response code="200">Si el alumno ha sido modificado</response>
        /// <response code="404">Si no existe un alumno con ese identificador</response>
        [HttpPut("{idAlumno}/{nombreAlumno}/{apellidosAlumno}/{fechaNacimiento}/{idPadre}")]
        public IActionResult Put(int idAlumno, string nombreAlumno, string apellidosAlumno, DateTime fechaNacimiento, int idPadre)
        {
            MAMASContext context = new MAMASContext();
            var alumno = context.Alumnos.SingleOrDefault(p => p.IdAlumno == idAlumno);
            if (alumno == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el alumno",
                    Detail = $"No existe el alumno con identificador: {idAlumno}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
            {
                alumno.NombreAlumno = nombreAlumno;
                alumno.ApellidosAlumnos = apellidosAlumno;
                alumno.FechaNacimientoAlumno = fechaNacimiento;
                alumno.IdPadre = idPadre;
                context.Alumnos.Update(alumno);
                context.SaveChanges();
                return Ok(alumno);
            }
        }
    }
}
