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
    public class ProfesoresController : ControllerBase
    {
        /// <summary>
        /// Devuelve los datos de todos los Profesores
        /// </summary> 
        /// <returns>El JSON de todos los Profesores</returns>
        /// <response code="200">Si existen Profesores</response>
        /// <response code="404">Si no existen Profesores</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();
            
            if (!context.Profesores.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay Profesores en la base de datos",
                    Detail = $"No hay Profesores en la tabla Profesores de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Profesores);
        }


        /// <summary>
        /// Devuelve los datos de un Profesor a partir de su identificador
        /// </summary> 
        /// <param name="idProfesor"> El identificador del Profesor</param>
        /// <returns>El JSON del Profesor</returns>
        /// <response code="200">Si el Profesor existe/response>
        /// <response code="404">Si el Profesor no existe</response>
        [HttpGet("{idProfesor}")]
        public IActionResult Get(int idProfesor)
        {
            MAMASContext context = new MAMASContext();
            var profesor = context.Profesores.SingleOrDefault(p => p.IdProfesor == idProfesor);
            if (profesor == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Id de Profesores inexistente",
                    Detail = $"No existe el id de Profesores: {idProfesor}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(profesor);
        }

        /// <summary>
        /// Devuelve los datos de un Profesor a partir de su identificador de usuario
        /// </summary> 
        /// <param name="idUsuario"> El identificador de usuario del Profesor</param>
        /// <param name="aux"> Una cadena auxiliar</param>
        /// <returns>El JSON del Profesor</returns>
        /// <response code="200">Si el Profesor existe/response>
        /// <response code="404">Si el Profesor no existe</response>
        [HttpGet("{idUsuario}/{aux}")]
        public IActionResult Get(int idUsuario, string aux)
        {
            MAMASContext context = new MAMASContext();
            var profesor = context.Profesores.SingleOrDefault(p => p.IdUsuario== idUsuario);
            if (profesor == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Id de Usuario de Profesor inexistente",
                    Detail = $"No existe el id de Usuario de Profesor: {idUsuario}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(profesor);
        }


        /// <summary>
        /// Crea un Profesor
        /// </summary> 
        /// <param name="nombreProfesor"> El nombre del Profesor</param>
        /// <param name="apellidosProfesor"> Los apellidos del Profesor</param>
        /// <param name="idUsuario"> El identificador de la cuenta de usuario</param>
        /// <returns>El JSON del Profesor</returns>
        /// <response code="201">Si el Profesor ha sido creado</response>
        /// <response code="400">Si el Profesor ya existe</response>
        [HttpPost("{nombreProfesor}/{apellidosProfesor}/{idUsuario}")]
        public IActionResult Post(string nombreProfesor, string apellidosProfesor, int idUsuario)
        {
            MAMASContext context = new MAMASContext();

            if (!context.Profesores.Any(p => p.IdUsuario == idUsuario))
            {
                Profesores profesor = new Profesores();
                profesor.NombreProfesor = nombreProfesor;
                profesor.ApellidosProfesor = apellidosProfesor;
                profesor.IdUsuario = idUsuario;
                context.Profesores.Add(profesor);
                context.SaveChanges();
                return CreatedAtRoute("", new { nombreProfesor = profesor.NombreProfesor, apellidosProfesor = profesor.ApellidosProfesor, idUsuario = profesor.IdUsuario }, profesor);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "Profesor ya existente",
                    Detail = $"Ya existe este Profesor",
                    Status = 400
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 400,
                };
            }
        }


        /// <summary>
        /// Borra un Profesor
        /// </summary>
        /// <param name="idProfesor"> El identificador del Profesor</param>
        /// <returns>El JSON del Profesor borrado</returns>
        /// <response code="200">Si el Profesor ha sido borrado</response>
        /// <response code="404">Si el Profesor no existe</response>
        /// <response code="400">Si el Profesor no puede ser borrado porque tiene Alumnos asignados</response>
        [HttpDelete("{idProfesor}")]
        public IActionResult Delete(int idProfesor)
        {
            MAMASContext context = new MAMASContext();
            var profesor = context.Profesores.SingleOrDefault(p => p.IdProfesor == idProfesor);
            if (profesor == null)
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
                if (profesor.AlumnoProfesor.Any())
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Tiene alumnos a su cargo",
                        Detail = $"El Profesor no puede ser borrado porque tiene Alumnos asignados",
                        Status = 400
                    };
                    return new ObjectResult(details)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = 400,
                    };
                }
                else
                {
                    context.Profesores.Remove(profesor);
                    context.SaveChanges();
                    return Ok(profesor);
                }
            }
        }


        /// <summary>
        /// Modifica un Profesor
        /// </summary> 
        /// <param name="idProfesor"> El identificador del Profesor</param>
        /// <param name="nombreProfesor"> El nombre del Profesor</param>
        /// <param name="apellidosProfesor"> Los apellidos del Profesor</param>
        /// <returns>El JSON del Profesor modificado</returns>
        /// <response code="200">Si el profesor ha sido modificado</response>
        /// <response code="404">Si no existe un profesor con ese identificador</response>
        [HttpPut("{idProfesor}/{nombreProfesor}/{apellidosProfesor}")]
        public IActionResult Put(int idProfesor, string nombreProfesor, string apellidosProfesor)
        {
            MAMASContext context = new MAMASContext();
            Profesores profesor = context.Profesores.SingleOrDefault(p => p.IdProfesor == idProfesor);
            if (profesor == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Profesor no existente",
                    Detail = $"No existe el profesor con identificador: {idProfesor}",
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
                profesor.NombreProfesor = nombreProfesor;
                profesor.ApellidosProfesor = apellidosProfesor;
                context.Profesores.Update(profesor);
                context.SaveChanges();
                return Ok(profesor);
            }
        }
    }
}
