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
    public class PadresController : ControllerBase
    {
        /// <summary>
        /// Devuelve todos los Padres
        /// </summary>       
        /// <returns>El JSON de los Padres</returns>
        /// <response code="200">Si hay Padres</response>
        /// <response code="404">Si no hay Padres en la base de datos</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();

            if (!context.Padres.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay Padres en la base de datos",
                    Detail = $"No hay Padres en la tabla Padres de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Padres);
        }


        /// <summary>
        /// Devuelve los datos de un padre a partir de su identificador
        /// </summary> 
        /// <param name="idPadre"> El identificador del padre</param>
        /// <returns>El JSON del padre</returns>
        /// <response code="200">Si el padre existe</response>
        /// <response code="404">Si el padre no existe</response>
        [HttpGet("PadrePorIdPadre/{idPadre}")]
        public IActionResult Get(int idPadre)
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
                return Ok(padre);
        }

        /// <summary>
        /// Devuelve los datos de un padre a partir de su identificador de usuario
        /// </summary> 
        /// <param name="idUsuario"> El identificador de usuario del padre </param>
        /// <param name="aux"> parametro auxiliar </param>
        /// <returns>El JSON del padre</returns>
        /// <response code="200">Si el padre existe</response>
        /// <response code="404">Si el padre no existe</response>
        [HttpGet("PadrePorIdUsu/{idUsuario}")]
        public IActionResult GetPadrePorUsu(int idUsuario)
        {
            MAMASContext context = new MAMASContext();
            var padre = context.Padres.SingleOrDefault(p => p.IdUsuario == idUsuario);
            if (padre == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Id de usuario de padre inexistente",
                    Detail = $"No existe el padre con identificador de usuario: {idUsuario}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(padre);
        }


        /// <summary>
        /// Crea un Padre
        /// </summary> 
        /// <param name="idUsuario"> El identificador del padre/madre</param>
        /// <param name="nombrePadre"> El nombre del padre/madre</param>
        /// <param name="apellidosPadre"> Los apellidos del padre/madre</param>
        /// <param name="direccionPadre"> La direccion del padre/madre</param>
        /// <param name="telefonoPadre"> El numero de telefono del padre/madre</param>
        /// <returns>El JSON del padre/madre</returns>
        /// <response code="200">Si existe al menos un padre/madre con ese nombre y apellidos</response>
        /// <response code="404">Si el padre/madre no existe</response>
        [HttpPost("{idUsuario}/{nombrePadre}/{apellidosPadre}/{direccionPadre}/{telefonoPadre}")]
        public IActionResult Post(int idUsuario, string nombrePadre, string apellidosPadre, string direccionPadre, string telefonoPadre)
        {
            MAMASContext context = new MAMASContext();

            if (!context.Padres.Any(p => p.IdUsuario == idUsuario))
            {
                Padres padre = new Padres();
                padre.NombrePadre = nombrePadre;
                padre.ApellidosPadre = apellidosPadre;
                padre.DireccionPadre = direccionPadre;
                padre.TelefonoPadre = telefonoPadre;
                padre.IdUsuario = idUsuario;
                context.Padres.Add(padre);
                context.SaveChanges();
                return CreatedAtRoute("", new { nombrePadre = padre.NombrePadre, apellidosPadre = padre.ApellidosPadre, direccionPadre = padre.DireccionPadre, telefonoPadre = padre.TelefonoPadre, idUsuario = padre.IdUsuario }, padre);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "Curso ya existente",
                    Detail = $"Ya existe este padre",
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
        /// Borra un Padre
        /// </summary> 
        /// <param name="idPadre"> El identificador del padre/madre</param>
        /// <returns>El JSON del padre/madre borrado</returns>
        /// <response code="200">Si el padre/madre ha sido borrado</response>
        /// <response code="404">Si el padre/madre no existe</response>
        [HttpDelete("{idPadre}")]
        public IActionResult Delete(int idPadre)
        {
            MAMASContext context = new MAMASContext();
            var padre = context.Padres.SingleOrDefault(p => p.IdPadre == idPadre);
            if (padre == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el padre",
                    Detail = $"No existe el padre con id: {idPadre}",
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
                if((padre.Alumnos).Count>0)
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Tiene alumnos asociados",
                        Detail = $"No se puede eliminar el padre porque hay alumnos asociados a él",
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
                    context.Padres.Remove(padre);
                    context.SaveChanges();
                    return Ok(padre);
                }
                
            }
        }

        /// <summary>
        /// Modifica un padre
        /// </summary>       
        /// <param name="idPadre"> El identificador del padre/madre</param>
        /// <param name="nombrePadre"> El nombre del padre/madre</param>
        /// <param name="apellidosPadre"> Los apellidos del padre/madre</param>
        /// <param name="direccionPadre"> La direccion del padre/madre</param>
        /// <param name="telefonoPadre"> El numero de telefono del padre/madre</param>
        /// <returns>El JSON del padre/madre modificado</returns>
        /// <response code="200">Si el padre/madre ha sido modificado</response>
        /// <response code="404">Si no existe un padre/madre con ese identificador</response>
        [HttpPut("{idPadre}/{nombrePadre}/{apellidosPadre}/{direccionPadre}/{telefonoPadre}")]
        public IActionResult Put(int idPadre, string nombrePadre, string apellidosPadre, string direccionPadre, string telefonoPadre)
        {
            MAMASContext context = new MAMASContext();
            Padres padre = context.Padres.SingleOrDefault(p => p.IdPadre == idPadre);
            if (padre == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Padre no existente",
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
                padre.NombrePadre = nombrePadre;
                padre.ApellidosPadre = apellidosPadre;
                padre.DireccionPadre = direccionPadre;
                padre.TelefonoPadre = telefonoPadre;
                context.Padres.Update(padre);
                context.SaveChanges();
                return Ok(padre);
            }
        }
    }
}
