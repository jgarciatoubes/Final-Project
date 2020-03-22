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
    public class ProfesorAsignaturaController : ControllerBase
    {

        /// <summary>
        /// Crea una relacion ProfesorAsignatura
        /// </summary> 
        /// <param name="idProfesor"> El identificador del profesor</param>
        /// <param name="idAsignatura"> El identificador de la asignatura</param>
        /// <returns>El JSON de la relacion ProfesorAsignatura</returns>
        /// <response code="201">Si la relacion ha sido creada</response>
        [HttpPost("{idProfesor}/{idAsignatura}")]
        public IActionResult Post(int idProfesor, int idAsignatura)
        {
            MAMASContext context = new MAMASContext();

            ProfesorAsignatura relacion = new ProfesorAsignatura();
            relacion.IdProfesor= idProfesor;
            relacion.IdAsignatura = idAsignatura;
            context.ProfesorAsignatura.Add(relacion);
            context.SaveChanges();
            return CreatedAtRoute("", new { idProfesor = relacion.IdProfesor, idAsignatura = relacion.IdAsignatura }, relacion);
        }


        /// <summary>
        /// Borra una o más relaciones ProfesorAsignatura
        /// </summary> 
        /// <param name="idProfesor"> El identificador del Profesor</param>
        /// <returns>El JSON de las relaciones ProfesorAsignatura</returns>
        /// <response code="201">Si las relaciones del Profesor han sido borradas</response>
        /// <response code="404">Si el Profesor no existe</response>
        [HttpDelete("{idProfesor}")]
        public IActionResult Delete(int idProfesor)
        {
            MAMASContext context = new MAMASContext();
            var relaciones = context.ProfesorAsignatura.Where(p => p.IdProfesor == idProfesor);
            if (!relaciones.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el Profesor",
                    Detail = $"No existe el Profesor con identificador: {idProfesor}",
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
                context.ProfesorAsignatura.RemoveRange(relaciones);
                context.SaveChanges();
                return Ok(relaciones);
            }

        }

    }
}
