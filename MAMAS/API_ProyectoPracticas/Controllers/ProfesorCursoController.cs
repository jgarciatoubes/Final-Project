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
    public class ProfesorCursoController : ControllerBase
    {

        /// <summary>
        /// Crea una relacion ProfesorCurso
        /// </summary> 
        /// <param name="idProfesor"> El identificador del profesor</param>
        /// <param name="idCurso"> El identificador del curso</param>
        /// <returns>El JSON de la relacion ProfesorCurso</returns>
        /// <response code="201">Si la relacion ha sido creada</response>
        [HttpPost("{idProfesor}/{idCurso}")]
        public IActionResult Post(int idProfesor, int idCurso)
        {
            MAMASContext context = new MAMASContext();

            ProfesorCurso relacion = new ProfesorCurso();
            relacion.IdProfesor= idProfesor;
            relacion.IdCurso = idCurso;
            context.ProfesorCurso.Add(relacion);
            context.SaveChanges();
            return CreatedAtRoute("", new { idProfesor = relacion.IdProfesor, idCurso = relacion.IdCurso }, relacion);
        }


        /// <summary>
        /// Borra una o más relaciones ProfesorCurso
        /// </summary> 
        /// <param name="idProfesor"> El identificador del Profesor</param>
        /// <returns>El JSON de las relaciones ProfesorCurso</returns>
        /// <response code="201">Si las relaciones del Profesor han sido borradas</response>
        /// <response code="404">Si el Profesor no existe</response>
        [HttpDelete("{idProfesor}")]
        public IActionResult Delete(int idProfesor)
        {
            MAMASContext context = new MAMASContext();
            var relaciones = context.ProfesorCurso.Where(p => p.IdProfesor == idProfesor);
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
                context.ProfesorCurso.RemoveRange(relaciones);
                context.SaveChanges();
                return Ok(relaciones);
            }

        }

    }
}
