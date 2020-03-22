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
    public class AsignaturasController : ControllerBase
    {
        /// <summary>
        /// Devuelve los datos de todas las asignaturas
        /// </summary> 
        /// <returns>El JSON de todas las asignaturas</returns>
        /// <response code="200">Si existen asignaturas</response>
        /// <response code="404">Si no existen asignaturas</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();

            if (!context.Asignaturas.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay asignaturas en la base de datos",
                    Detail = $"No hay asignaturas en la tabla Asignaturas de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Asignaturas);
        }


        /// <summary>
        /// Busca una asignatura
        /// </summary>       
        /// <param name="nombreAsignatura"> El nombre de la asignatura</param>
        /// <returns>El JSON de la Asignatura</returns>
        /// <response code="200">Si el item existe</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("{nombreAsignatura}")]
        public IActionResult Get(string nombreAsignatura)
        {
            MAMASContext context = new MAMASContext();
            var asignatura = context.Asignaturas.SingleOrDefault(p => p.NombreAsignatura == nombreAsignatura); 
            if (asignatura == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la asignatura",
                    Detail = $"No existe la asignatura: {nombreAsignatura}",
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
                return Ok(asignatura);
            }      
        }

        /// <summary>
        /// Busca una asignatura
        /// </summary>       
        /// <param name="idAsignatura"> El id de la asignatura</param>
        /// <returns>El JSON de la Asignatura</returns>
        /// <response code="200">Si el item existe</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("AsigPorId/{idAsignatura}")]
        public IActionResult Get(int idAsignatura)
        {
            MAMASContext context = new MAMASContext();
            var asignatura = context.Asignaturas.SingleOrDefault(p => p.IdAsignatura == idAsignatura);
            if (asignatura == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la asignatura",
                    Detail = $"No existe la asignatura de id: {idAsignatura}",
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
                return Ok(asignatura);
            }
        }

        /// <summary>
        /// Inserta una asignatura
        /// </summary>
        /// <param name="nombreAsignatura"> El nombre de la asignatura</param>
        /// <returns>El JSON de la nueva Asignatura creada</returns>
        /// <response code="201">Si el item ha sido creado</response>
        /// <response code="400">Si el item ya existe</response>
        [HttpPost("{nombreAsignatura}")]
        public IActionResult Post(string nombreAsignatura)
        {
            MAMASContext context = new MAMASContext();
            if (!context.Asignaturas.Any(p => p.NombreAsignatura == nombreAsignatura))
            {
                Asignaturas asignatura = new Asignaturas();
                asignatura.NombreAsignatura = nombreAsignatura;
                context.Asignaturas.Add(asignatura);
                context.SaveChanges();
                return CreatedAtRoute("", new { nombreAsignatura = asignatura.NombreAsignatura }, asignatura);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "Asignatura ya existente",
                    Detail = $"Ya existe la asignatura de: {nombreAsignatura}",
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
        /// Borra una asignatura
        /// </summary>
        /// <param name="nombreAsignatura"> El nombre de la asignatura</param>
        /// <returns>El JSON de la Asignatura borrada</returns>
        /// <response code="200">Si el item ha sido borrado</response>
        /// <response code="404">Si el item no existe</response>
        [HttpDelete("{nombreAsignatura}")]
        public IActionResult Delete(string nombreAsignatura)
        {
            MAMASContext context = new MAMASContext();
            var asignatura = context.Asignaturas.SingleOrDefault(p => p.NombreAsignatura == nombreAsignatura);
            if (asignatura == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la asignatura",
                    Detail = $"No existe la asignatura: {nombreAsignatura}",
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
                if(asignatura.ProfesorAsignatura.Count != 0 || asignatura.Notas.Count != 0)
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Hay profesores que imparten esta asignatura o hay notas puestas para esa asignatura",
                        Detail = $"No se puede borrar la asignatura {nombreAsignatura} porque hay profesores que la imparten o la asignatura tiene notas",
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
                    context.Asignaturas.Remove(asignatura);
                    context.SaveChanges();
                    return Ok(asignatura);
                }      
            }
        }

        /// <summary>
        /// Modifica una asignatura
        /// </summary>
        /// <param name="idAsignatura"> El identificador de la asignatura</param>
        /// <param name="nombreAsignatura"> El nuevo nombre de la asignatura</param>
        /// <returns>El JSON de la Asignatura modificada</returns>
        /// <response code="200">Si la asignatura ha sido modificada</response>
        /// <response code="400">Si ya existe una asignatura con ese nombre</response>
        /// <response code="404">Si ya asignatura no existe</response>
        [HttpPut("{idAsignatura, nombreAsignatura}")]
        public IActionResult Put(int idAsignatura, string nombreAsignatura)
        {
            MAMASContext context = new MAMASContext();
            Asignaturas asignatura = context.Asignaturas.SingleOrDefault(p => p.IdAsignatura == idAsignatura);
            if(asignatura == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Asignatura no existente",
                    Detail = $"No existe la asignatura con identificador: {idAsignatura}",
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
                if(!context.Asignaturas.Any(p => p.NombreAsignatura == nombreAsignatura))
                {
                    asignatura.NombreAsignatura = nombreAsignatura;
                    context.Asignaturas.Update(asignatura);
                    context.SaveChanges();
                    return Ok(asignatura);
                }
                else
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Ya existe una asignatura con ese nombre",
                        Detail = $"Ya existe la asignatura de: {nombreAsignatura}",
                        Status = 400
                    };
                    return new ObjectResult(details)
                    {
                        ContentTypes = { "application/problem+json" },
                        StatusCode = 400,
                    };
                }
            }
        }
    }
}
