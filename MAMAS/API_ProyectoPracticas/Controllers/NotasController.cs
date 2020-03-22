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
    public class NotasController : ControllerBase
    {
        /// <summary>
        /// Devuelve las Notas de un alumno
        /// </summary> 
        /// <param name="idAlumno"> El identificador del padre del alumno</param>
        /// <returns>El JSON de las Notas de un alumno</returns>
        /// <response code="200">Si existe el alumno y tiene Notas</response>
        /// <response code="404">Si no existe el alumno</response>
        /// <response code="400">Si no tiene Notas</response>
        [HttpGet("{idAlumno}")]
        public IActionResult Get(int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            var notas = context.Notas.Where(p => p.IdAlumno == idAlumno);
            var alumno = context.Alumnos.SingleOrDefault(p => p.IdAlumno == idAlumno);
            if(alumno == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Id de alumno inexistente",
                    Detail = $"No existe el id de alumno: {idAlumno}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            if (!notas.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "El alumno no tiene Notas",
                    Detail = $"El alumno con identificador : {idAlumno} aún no tiene Notas en la base de datos.",
                    Status = 400
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 400,
                };
            }
            else
                return Ok(notas);
        }

        /// <summary>
        /// Devuelve una Nota a partir de sus valores
        /// </summary> 
        /// <param name="notaValor"> El valor de la nota</param>
        /// <param name="idAsignatura"> El identificador de la asignatura</param>
        /// <param name="idEvaluacion"> El identificador de la evaluación</param>
        /// <param name="idCurso"> El identificador del curso</param>
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <returns>El JSON de la nota</returns>
        /// <response code="201">Si la nota ha existe</response>
        /// <response code="404">Si no existe</response>
        [HttpGet("{idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}")]
        public IActionResult Get(int idAsignatura, int idEvaluacion, int idCurso, int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            Notas nota = context.Notas.SingleOrDefault(p => p.IdAsignatura == idAsignatura && p.IdEvaluacion == idEvaluacion && p.IdCurso == idCurso && p.IdAlumno == idAlumno);
            if (nota == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "La nota no existe",
                    Detail = $" La nota para la asignatura de id {idAsignatura}, en la evaluacion de id {idEvaluacion} del curso con id {idCurso}, no existe",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(nota);
        }

        /// <summary>
        /// Crea una Nota
        /// </summary> 
        /// <param name="notaValor"> El valor de la nota</param>
        /// <param name="idAsignatura"> El identificador de la asignatura</param>
        /// <param name="idEvaluacion"> El identificador de la evaluación</param>
        /// <param name="idCurso"> El identificador del curso</param>
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <returns>El JSON de las nota creada</returns>
        /// <response code="201">Si la nota ha sido creado</response>
        /// <response code="400">Si ya existe una nota con los mismos identificadores</response>
        [HttpPost("{notaValor}/{idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}")]
        public IActionResult Post(string notaValor, int idAsignatura, int idEvaluacion, int idCurso, int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            if(!context.Notas.Any(p => p.IdAsignatura == idAsignatura && p.IdEvaluacion == idEvaluacion && p.IdCurso == idCurso && p.IdAlumno == idAlumno))
            {
                Notas nota = new Notas();
                nota.Nota = notaValor;
                nota.IdAsignatura = idAsignatura;
                nota.IdEvaluacion = idEvaluacion;
                nota.IdCurso = idCurso;
                nota.IdAlumno = idAlumno;
                context.Notas.Add(nota);
                context.SaveChanges();
                return CreatedAtRoute("", new
                {
                    notaValor = nota.Nota,
                    idAsignatura = nota.IdAsignatura,
                    idEvaluacion = nota.IdEvaluacion,
                    idCurso = nota.IdCurso,
                    idAlumno = nota.IdAlumno
                }, nota);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "La nota ya existe",
                    Detail = $"El alumno con id {idAlumno} ya tiene nota de valor {notaValor} para la asignatura de id {idAsignatura}, en la evaluacion de id {idEvaluacion} del curso con id {idCurso}",
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
        /// Borra una nota
        /// </summary>
        /// <param name="idNota"> El identificador de la nota</param>
        /// <returns>El JSON de la nota borrado</returns>
        /// <response code="200">Si el item ha sido borrado</response>
        /// <response code="404">Si el item no existe</response>
        [HttpDelete("{idNota}")]
        public IActionResult Delete(int idNota)
        {
            MAMASContext context = new MAMASContext();
            var nota = context.Notas.SingleOrDefault(p => p.IdNota == idNota);
            if (nota == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe la nota",
                    Detail = $"No existe la nota con identificador: {idNota}",
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
                context.Notas.Remove(nota);
                context.SaveChanges();
                return Ok(nota);
            }

        }

        /// <summary>
        /// Modifica una nota
        /// </summary>       
        /// <param name="idNota"> El identificador de la nota</param>
        /// <param name="notaValor"> El valor de la nota</param>
        /// <param name="idAsignatura"> El identificador de la asignatura</param>
        /// <param name="idEvaluacion"> El identificador de la evaluación</param>
        /// <param name="idCurso"> El identificador del curso</param>
        /// <param name="idAlumno"> El identificador del alumno</param>
        /// <response code="200">Si la nota ha sido modificado</response>
        /// <response code="400">Si ya existe una nota con el mismo notaValor, idAsignatura, idEvaluacion, idCurso e idAlumno</response>
        /// <response code="404">Si no existe una nota con ese identificador</response>
        [HttpPut("{idNota}/{notaValor}/{idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}")]
        public IActionResult Put(int idNota, string notaValor, int idAsignatura, int idEvaluacion, int idCurso, int idAlumno)
        {
            MAMASContext context = new MAMASContext();
            Notas nota = context.Notas.SingleOrDefault(p => p.IdNota == idNota);
            if (nota == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Nota no existente",
                    Detail = $"No existe la nota con identificador: {idNota}",
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
                if (!context.Notas.Any(p => p.IdAsignatura == idAsignatura && p.IdEvaluacion == idEvaluacion && p.IdCurso == idCurso && p.IdAlumno == idAlumno && p.Nota == notaValor))
                {
                    nota.Nota = notaValor;
                    nota.IdAsignatura = idAsignatura;
                    nota.IdEvaluacion = idEvaluacion;
                    nota.IdCurso = idCurso;
                    nota.IdAlumno = idAlumno;
                    context.Notas.Update(nota);
                    context.SaveChanges();
                    return Ok(nota);
                }
                else
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Ya existe una nota con el mismo notaValor, idAsignatura, idEvaluacion, idCurso e idAlumno",
                        Detail = $"Ya existe la nota de valor: {notaValor} e identificadores: {idAsignatura}/{idEvaluacion}/{idCurso}/{idAlumno}",
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
