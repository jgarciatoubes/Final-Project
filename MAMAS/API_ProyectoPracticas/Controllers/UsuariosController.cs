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
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Devuelve los datos de todos los Usuarios
        /// </summary> 
        /// <returns>El JSON de todos los Usuarios</returns>
        /// <response code="200">Si existen Usuarios</response>
        /// <response code="404">Si no existen Usuarios</response>
        [HttpGet()]
        public IActionResult Get()
        {
            MAMASContext context = new MAMASContext();

            if (!context.Usuarios.Any())
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay Usuarios en la base de datos",
                    Detail = $"No hay Usuarios en la tabla Usuarios de la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(context.Usuarios);
        }

        /// <summary>
        /// A partir de un nombre de usuario y una contraseña devuelve los datos del usuario
        /// </summary> 
        /// <param name="nombreUsuario"> El username del usuario</param>
        /// <param name="passUsuario"> La password del usuario</param>
        /// <returns>El JSON del usuario</returns>
        /// <response code="200">Devuelve el item</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("{nombreUsuario}/{passUsuario}")]
        public IActionResult GetConPass(string nombreUsuario, string passUsuario)
        {
            MAMASContext context = new MAMASContext();
            var Usuario = context.Usuarios.SingleOrDefault(p => p.UsernameUsuario == nombreUsuario && p.PassUsuario == passUsuario);
            if (Usuario == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Contraseña incorrecta",
                    Detail = $"No existe la password: {passUsuario}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404
                };
            }
            else
                return Ok(Usuario);                             
        }


        /// <summary>
        /// Devuelve los datos de un usuario a partir de su nombre de usuario
        /// </summary> 
        /// <param name="nombreUsuario"> El username del usuario</param>
        /// <returns>El JSON del usuario</returns>
        /// <response code="200">Devuelve el item</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("{nombreUsuario}")]
        public IActionResult Get(string nombreUsuario)
        {
            MAMASContext context = new MAMASContext();
            var Usuario = context.Usuarios.SingleOrDefault(p => p.UsernameUsuario == nombreUsuario);
            if (Usuario == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Username incorrecto",
                    Detail = $"No existe el username: {nombreUsuario}",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }
            else
                return Ok(Usuario);
        }

        /// <summary>
        /// Devuelve el usuario con mayor ID de Usuario
        /// </summary> 
        /// <returns>El JSON del usuario con mayor ID</returns>
        /// <response code="200">Si existe el item</response>
        /// <response code="404">Si el item no existe</response>
        [HttpGet("MayorID")]
        public IActionResult GetMayorID()
        {
            MAMASContext context = new MAMASContext();
            try
            {
                var idUsuario = context.Usuarios.Max(p => p.IdUsuario);
                var usuario = context.Usuarios.SingleOrDefault(p => p.IdUsuario == idUsuario);
                return Ok(usuario);
            }
            catch
            {
                var details = new ProblemDetails()
                {
                    Title = "No hay usuarios",
                    Detail = $"No hay usuarios en la base de datos",
                    Status = 404
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 404,
                };
            }           
        }    

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="nombreUsuario"> El nombre de usuario</param>
        ///<param name="passUsuario"> La password de la cuenta de usuario</param>
        /// <param name="idPerfil"> El id del tipo de perfil</param>
        /// <returns>El JSON de la nueva cuenta de usuario creada</returns>
        /// <response code="201">Si el item ha sido creado</response>
        /// <response code="400">Si el item ya existe</response>
        [HttpPost("{nombreUsuario}/{passUsuario}/{idPerfil}")]
        public IActionResult Post(string nombreUsuario, string passUsuario, int idPerfil)
        {
            MAMASContext context = new MAMASContext();
            if (!context.Usuarios.Any(p => p.UsernameUsuario == nombreUsuario))
            {
                Usuarios usuario = new Usuarios();
                usuario.UsernameUsuario = nombreUsuario;
                usuario.PassUsuario = passUsuario;
                usuario.IdPerfil = idPerfil;
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return CreatedAtRoute("", new { nombreUsuario = usuario.UsernameUsuario, passUsuario = usuario.PassUsuario, idPerfil = usuario.IdPerfil}, usuario);
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Title = "Nombre de usuario ya existente",
                    Detail = $"Ya existe el usuario: {nombreUsuario}",
                    Status = 400
                };
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 400,
                };
            }
        }

        /*
        /// <summary>
        /// Borra un usuario
        /// </summary>
        /// <param name="nombreUsuario"> El nombre de la cuenta de usuario</param>
        /// <returns>El JSON del usuario borrado</returns>
        /// <response code="200">Si el item ha sido borrado</response>
        /// <response code="404">Si el item no existe</response>
        [HttpDelete("{nombreUsuario}")]
        public IActionResult Delete(string nombreUsuario)
        {
            MAMASContext context = new MAMASContext();
            var usuario = context.Usuarios.SingleOrDefault(p => p.UsernameUsuario == nombreUsuario);
            if (usuario == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el usuario",
                    Detail = $"No existe el usuario de nombre: {nombreUsuario}",
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
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return Ok(usuario);      
            }
        }*/

        /// <summary>
        /// Borra un usuario
        /// </summary>
        /// <param name="idUsuario"> El identificador de la cuenta de usuario</param>
        /// <returns>El JSON del usuario borrado</returns>
        /// <response code="200">Si el item ha sido borrado</response>
        /// <response code="404">Si el item no existe</response>
        [HttpDelete("{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            MAMASContext context = new MAMASContext();
            var usuario = context.Usuarios.SingleOrDefault(p => p.IdUsuario == idUsuario);
            if (usuario == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "No existe el usuario",
                    Detail = $"No existe el usuario de id: {idUsuario}",
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
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return Ok(usuario);
            }
        }

        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="idUsuario"> El identificador de usuario</param>
        /// <param name="nombreUsuario"> El nombre de usuario</param>
        /// <param name="passUsuario"> La password de la cuenta de usuario</param>
        /// <param name="idPerfil"> El id del tipo de perfil</param>
        /// <returns>El JSON de la cuenta de usuario modificada</returns>
        /// <response code="200">Si el usuario ha sido modificado</response>
        /// <response code="400">Si ya existe un usuario con ese nombre de usuario</response>
        /// <response code="404">Si no existe un usuario con ese identificador</response>
        [HttpPut("{idUsuario}/{nombreUsuario}/{passUsuario}/{idPerfil}")]
        public IActionResult Put(int idUsuario, string nombreUsuario, string passUsuario, int idPerfil)
        {
            MAMASContext context = new MAMASContext();
            Usuarios usuario = context.Usuarios.SingleOrDefault(p => p.IdUsuario == idUsuario);
            if (usuario == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Usuario no existente",
                    Detail = $"No existe el usuario con identificador: {idUsuario}",
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
                if (!context.Usuarios.Any(p => p.UsernameUsuario == nombreUsuario))
                {
                    usuario.UsernameUsuario = nombreUsuario;
                    usuario.PassUsuario = passUsuario;
                    usuario.IdPerfil = idPerfil;
                    context.Usuarios.Update(usuario);
                    context.SaveChanges();
                    return Ok(usuario);
                }
                else
                {
                    var details = new ProblemDetails()
                    {
                        Title = "Ya existe un usuario con ese nombre de usuario",
                        Detail = $"Ya existe el usuario de nombre: {nombreUsuario}",
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

        /// <summary>
        /// Modifica un usuario
        /// </summary>
        /// <param name="idUsuario"> El identificador de usuario</param>
        /// <param name="passUsuario"> La password de la cuenta de usuario</param>
        /// <param name="idPerfil"> El id del tipo de perfil</param>
        /// <returns>El JSON de la cuenta de usuario modificada</returns>
        /// <response code="200">Si el usuario ha sido modificado</response>
        /// <response code="404">Si no existe un usuario con ese identificador</response>
        [HttpPut("{idUsuario}/{passUsuario}/{idPerfil}")]
        public IActionResult Put(int idUsuario, string passUsuario, int idPerfil)
        {
            MAMASContext context = new MAMASContext();
            Usuarios usuario = context.Usuarios.SingleOrDefault(p => p.IdUsuario == idUsuario);
            if (usuario == null)
            {
                var details = new ProblemDetails()
                {
                    Title = "Usuario no existente",
                    Detail = $"No existe el usuario con identificador: {idUsuario}",
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
                usuario.PassUsuario = passUsuario;
                usuario.IdPerfil = idPerfil;
                context.Usuarios.Update(usuario);
                context.SaveChanges();
                return Ok(usuario);
            }
        }
    }
}
