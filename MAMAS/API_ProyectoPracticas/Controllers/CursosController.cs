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
    public class CursosController : ControllerBase
    {
        /// <summary>
        /// Devuelve los datos de todos los cursos
        /// </summary> 
        /// <returns>El JSON de todos los cursos</returns>
        /// <response code="200">Si existen cursos</response>
        /// <response code="404">Si no existen cursos</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();

            if (!context.Cursos.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay cursos en la base de datos",
                    Detail = $"No hay Cursos en la tabla Cursos de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Cursos);
        }

        /// <summary>
        /// Busca un curso
        /// </summary>       
        /// <param name="idCurso"> El id del curso</param>
        /// <returns>El JSON del curso</returns>
        /// <response code="200">Si el curso existe</response>
        /// <response code="404">Si el curso no existe</response>
        [HttpGet("{idCurso}")]
        public IActionResult Get(int idCurso)
        {
            MAMASContext context = new MAMASContext();
            var curso = context.Cursos.SingleOrDefault(p => p.IdCurso == idCurso);
            if (curso == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el curso",
                    Detail = $"No existe el curso de id: {idCurso}",
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
                return Ok(curso);
            }
        }

        /// <summary>
        /// Busca un curso
        /// </summary>       
        /// <param name="numeroCurso"> El número del curso</param>
        /// <param name="letraCurso"> La letra del curso</param>
        /// <returns>El JSON del curso</returns>
        /// <response code="200">Si el curso existe</response>
        /// <response code="404">Si el curso no existe</response>
        [HttpGet("{numeroCurso}/{letraCurso}")]
        public IActionResult Get(int numeroCurso, string letraCurso)
        {
            MAMASContext context = new MAMASContext();
            var curso = context.Cursos.SingleOrDefault(p => p.NumeroCurso == numeroCurso && p.LetraCurso == letraCurso); 
            if (curso == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el curso",
                    Detail = $"No existe el curso: {numeroCurso}{letraCurso}",
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
                return Ok(curso);
            }      
        }

        /// <summary>
        /// Crea un curso
        /// </summary>       
        /// <param name="numeroCurso"> El número del curso</param>
        /// <param name="letraCurso"> La letra del curso</param>
        /// <returns>El JSON del curso</returns>
        /// <response code="201">Si el curso ha sido creado</response>
        /// <response code="400">Si el curso ya existe</response>
        [HttpPost("{numeroCurso}/{letraCurso}")]
        public IActionResult Post(int numeroCurso, string letraCurso)
        {
            MAMASContext context = new MAMASContext();
            if (!context.Cursos.Any(p => p.NumeroCurso == numeroCurso && p.LetraCurso == letraCurso))
            {
                Cursos curso = new Cursos();
                curso.NumeroCurso = numeroCurso;
                curso.LetraCurso = letraCurso;
                context.Cursos.Add(curso);
                context.SaveChanges();
                return CreatedAtRoute("", new { numeroCurso = curso.NumeroCurso, letraCurso = curso.LetraCurso }, curso);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "Curso ya existente",
                    Detail = $"Ya existe el curso: {numeroCurso}{letraCurso}",
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
        /// Borra un curso
        /// </summary>       
        /// <param name="numeroCurso"> El número del curso</param>
        /// <param name="letraCurso"> La letra del curso</param>
        /// <returns>El JSON del curso eliminado</returns>
        /// <response code="200">Si el curso ha sido eliminado</response>
        /// <response code="404">Si el curso no existe</response>
        [HttpDelete("{numeroCurso}/{letraCurso}")]
        public IActionResult Delete(int numeroCurso, string letraCurso)
        {
            MAMASContext context = new MAMASContext();
            var curso = context.Cursos.SingleOrDefault(p => p.NumeroCurso == numeroCurso && p.LetraCurso == letraCurso);
            if (curso == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el curso",
                    Detail = $"No existe el curso: {numeroCurso}{letraCurso}",
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
                if(curso.ProfesorCurso.Count != 0 || curso.Notas.Count != 0)
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Hay profesores asociados",
                        Detail = $"No se puede eliminar el curso porque hay profesores asociados al curso: {numeroCurso}{letraCurso}",
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
                    context.Cursos.Remove(curso);
                    context.SaveChanges();
                    return Ok(curso);
                }
                
            }
        }

        /// <summary>
        /// Modifica un curso
        /// </summary>       
        /// <param name="idCurso"> El identificador del curso</param>
        /// <param name="numeroCurso"> El número del curso</param>
        /// <param name="letraCurso"> La letra del curso</param>
        /// <returns>El JSON del curso modificado</returns>
        /// <response code="200">Si el curso ha sido modificado</response>
        /// <response code="400">Si ya existe un curso con ese numero y letra</response>
        /// <response code="404">Si no existe un curso con ese identificador</response>
        [HttpPut("{idCurso}/{numeroCurso}/{letraCurso}")]
        public IActionResult Put(int idCurso, int numeroCurso, string letraCurso)
        {
            MAMASContext context = new MAMASContext();
            Cursos curso = context.Cursos.SingleOrDefault(p => p.IdCurso == idCurso);
            if (curso == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Curso no existente",
                    Detail = $"No existe el curso con identificador: {idCurso}",
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
                if (!context.Cursos.Any(p => p.NumeroCurso == numeroCurso && p.LetraCurso == letraCurso))
                {
                    curso.NumeroCurso = numeroCurso;
                    curso.LetraCurso = letraCurso;
                    context.Cursos.Update(curso);
                    context.SaveChanges();
                    return Ok(curso);
                }
                else
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Ya existe un curso con ese número y letra",
                        Detail = $"Ya existe el curso de: {numeroCurso}{letraCurso}",
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
