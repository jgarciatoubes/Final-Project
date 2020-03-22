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
    public class AlumnoProfesorController : ControllerBase
    {
        /// <summary>
        /// Devuelve los datos las relaciónes AlumnoProfesor a partir de su id de Profesor
        /// </summary> 
        /// <param name="idProfesor"> El id del Profesor</param>
        /// <returns>El JSON de la Relacion</returns>
        /// <response code="200">Si existen relaciones</response>
        /// <response code="404">Si no existen relaciones</response>
        [HttpGet("{idProfesor}")]
        public IActionResult Get(int idProfesor)
        {
            MAMASContext context = new MAMASContext();
            var relaciones = context.AlumnoProfesor.Where(p => p.IdProfesor == idProfesor);
            if (!relaciones.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "Id de Profesor incorrecto",
                    Detail = $"No existe el profesor con id: {idProfesor}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(relaciones);
        }

        /// <summary>
        /// Crea una relacion AlumnoProfesor
        /// </summary> 
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <param name="idProfesor"> El identificador del profesor</param>
        /// <returns>El JSON de la relacion AlumnoProfesor</returns>
        /// <response code="201">Si la relacion ha sido creada</response>
        [HttpPost("{idAlumno}/{idProfesor}")]
        public IActionResult Post(int idAlumno, int idProfesor)
        {
            MAMASContext context = new MAMASContext();

            AlumnoProfesor relacion = new AlumnoProfesor();
            relacion.IdAlumno= idAlumno;
            relacion.IdProfesor = idProfesor;
            context.AlumnoProfesor.Add(relacion);
            context.SaveChanges();
            return CreatedAtRoute("", new { idAlumno = relacion.IdAlumno, idProfesor = relacion.IdProfesor }, relacion);
        }


        /// <summary>
        /// Borra una o más relaciones AlumnoProfesor
        /// </summary> 
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <returns>El JSON de las relaciones AlumnoProfesor</returns>
        /// <response code="201">Si las relaciones del Alumno han sido borradas</response>
        /// <response code="404">Si el alumno no existe</response>
        [HttpDelete("{idAlumno}")]
        public IActionResult Delete(int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            var relaciones = context.AlumnoProfesor.Where(p => p.IdAlumno == idAlumno);
            if (!relaciones.Any())
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
                context.AlumnoProfesor.RemoveRange(relaciones);
                context.SaveChanges();
                return Ok(relaciones);
            }

        }

    }
}
