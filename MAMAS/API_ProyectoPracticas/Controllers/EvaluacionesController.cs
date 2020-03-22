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
    public class EvaluacionesController : ControllerBase
    {
        /// <summary>
        /// Devuelve todas las Evaluaciones
        /// </summary>       
        /// <returns>El JSON de las Evaluaciones</returns>
        /// <response code="200">Si hay Evaluaciones</response>
        /// <response code="404">Si no hay Evaluaciones en la base de datos</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();
            if(!context.Evaluaciones.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay Evaluaciones en la base de datos",
                    Detail = $"No hay Evaluaciones en la tabla Evaluaciones de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Evaluaciones);
        }


        /// <summary>
        /// Busca una Evaluacion
        /// </summary>       
        /// <param name="nombreEvaluacion"> El nombre de la Evaluacion</param>
        /// <returns>El JSON de la Evaluacion</returns>
        /// <response code="200">Si el item existe</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("{nombreEvaluacion}")]
        public IActionResult Get(string nombreEvaluacion)
        {
            MAMASContext context = new MAMASContext();
            var evaluacion = context.Evaluaciones.SingleOrDefault(p => p.NombreEvaluacion == nombreEvaluacion); 
            if (evaluacion == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la Evaluacion",
                    Detail = $"No existe la Evaluacion: {nombreEvaluacion}",
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
                return Ok(evaluacion);
            }      
        }

        /// <summary>
        /// Busca una Evaluacion
        /// </summary>       
        /// <param name="idEvaluacion"> El id de la Evaluacion</param>
        /// <param name="aux"> Parámetro auxiliar</param>
        /// <returns>El JSON de la Evaluacion</returns>
        /// <response code="200">Si el item existe</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("EvalPorId/{idEvaluacion}")]
        public IActionResult Get(int idEvaluacion)
        {
            MAMASContext context = new MAMASContext();
            var evaluacion = context.Evaluaciones.SingleOrDefault(p => p.IdEvaluacion == idEvaluacion);
            if (evaluacion == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la Evaluacion",
                    Detail = $"No existe la Evaluacion de id: {idEvaluacion}",
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
                return Ok(evaluacion);
            }
        }

        /// <summary>
        /// Inserta una Evaluacion
        /// </summary>
        /// <param name="nombreEvaluacion"> El nombre de la Evaluacion</param>
        /// <returns>El JSON de la nueva Evaluacion creada</returns>
        /// <response code="201">Si el item ha sido creado</response>
        /// <response code="400">Si el item ya existe</response>
        [HttpPost("{nombreEvaluacion}")]
        public IActionResult Post(string nombreEvaluacion)
        {
            MAMASContext context = new MAMASContext();
            if (!context.Evaluaciones.Any(p => p.NombreEvaluacion == nombreEvaluacion))
            {
                Evaluaciones evaluacion = new Evaluaciones();
                evaluacion.NombreEvaluacion = nombreEvaluacion;
                context.Evaluaciones.Add(evaluacion);
                context.SaveChanges();
                return CreatedAtRoute("", new { nombreEvaluacion = evaluacion.NombreEvaluacion}, evaluacion);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "Evaluacion ya existente",
                    Detail = $"Ya existe la Evaluacion de: {nombreEvaluacion}",
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
        /// Borra una Evaluacion
        /// </summary>
        /// <param name="nombreEvaluacion"> El nombre de la Evaluacion</param>
        /// <returns>El JSON de la Evaluacion borrada</returns>
        /// <response code="200">Si el item ha sido borrado</response>
        /// <response code="404">Si el item no existe</response>
        [HttpDelete("{nombreEvaluacion}")]
        public IActionResult Delete(string nombreEvaluacion)
        {
            MAMASContext context = new MAMASContext();
            var evaluacion = context.Evaluaciones.SingleOrDefault(p => p.NombreEvaluacion == nombreEvaluacion);
            if (evaluacion == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la Evaluacion",
                    Detail = $"No existe la Evaluacion: {nombreEvaluacion}",
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
                if(evaluacion.Notas.Count != 0)
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Hay notas en esta Evaluacion",
                        Detail = $"No se puede borrar la Evaluacion {nombreEvaluacion} porque hay notas en ella",
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
                    context.Evaluaciones.Remove(evaluacion);
                    context.SaveChanges();
                    return Ok(evaluacion);
                }      
            }
        }

        /// <summary>
        /// Modifica una evaluación
        /// </summary>       
        /// <param name="idEvaluacion"> El identificador de la evaluación</param>
        /// <param name="nombreEvaluacion"> El nombre de la evaluación</param>
        /// <returns>El JSON de la evaluación modificada</returns>
        /// <response code="200">Si la evaluación ha sido modificado</response>
        /// <response code="400">Si ya existe una evaluación con ese nombre</response>
        /// <response code="404">Si no existe una evaluación con ese nombre</response>
        [HttpPut("{idEvaluacion}/{nombreEvaluacion}")]
        public IActionResult Put(int idEvaluacion, string nombreEvaluacion)
        {
            MAMASContext context = new MAMASContext();
            Evaluaciones evaluacion = context.Evaluaciones.SingleOrDefault(p => p.IdEvaluacion == idEvaluacion);
            if (evaluacion == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Evaluacion no existente",
                    Detail = $"No existe la evaluacion con identificador: {idEvaluacion}",
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
                if (!context.Evaluaciones.Any(p => p.NombreEvaluacion == nombreEvaluacion))
                {
                    evaluacion.NombreEvaluacion = nombreEvaluacion;
                    context.Evaluaciones.Update(evaluacion);
                    context.SaveChanges();
                    return Ok(evaluacion);
                }
                else
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Ya existe una evaluación con ese nombre",
                        Detail = $"Ya existe la evaluación de nombre: {nombreEvaluacion}",
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
