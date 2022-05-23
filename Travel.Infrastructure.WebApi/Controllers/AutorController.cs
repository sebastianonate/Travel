using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Core.Application.Features.Autores.Commands;
using Travel.Core.Application.Features.Autores.Commands.Create;
using Travel.Core.Application.Features.Autores.Commands.Delete;
using Travel.Core.Application.Features.Autores.Commands.Update;
using Travel.Core.Application.Features.Autores.Queries;
using Travel.Core.Application.Features.Libros.Queries;

namespace Travel.Infrastructure.WebApi.Controllers
{
    public class AutorController : BaseApiController<AutorController>
    {
        
        /// <summary>
        /// Metodo para consultar todos los autores
        /// </summary>
        /// <returns>Autores</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllAutoresRequest());
            return Ok(response.Autores);
        }

        /// <summary>
        /// Metodo para consultar un autor por id
        /// </summary>
        /// <param name="autorId">Id del autor.</param>
        /// <returns>Autor</returns>
        [HttpGet("{autorId}")]
        public async Task<IActionResult> GetById(int autorId)
        {
            var response = await _mediator.Send(new GetAutorByIdRequest(autorId));
            return Ok(response.Autor);
        }
        
        /// <summary>
        /// Metodo para guardar un autor
        /// </summary>
        /// <param name="request">Request con los datos del autor.</param>
        /// <returns>Autor</returns>   
        [HttpPost]
        public async Task<IActionResult> Post(CreateAutorRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Metodo para actualizar el autor
        /// </summary>
        /// <param name="autorId">Id del autor.</param>
        /// <param name="request">Request con los datos para actualizar el autor.</param>
        /// <returns>Respuesta actualizar autor</returns>
        [HttpPut("{autorId}")]
        public async Task<IActionResult> Put(int autorId,UpdateAutorRequest request)
        {
            if (autorId != request.AutorId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(request));
        }
        
        /// <summary>
        /// Metodo para eliminar el autor
        /// </summary>
        /// <param name="autorId">Id del autor.</param>
        /// <returns>Respuesta eliminación</returns>
        [HttpDelete("{autorId}")]
        public async Task<IActionResult> Delete(int autorId)
        {
            var response = await _mediator.Send(new DeleteAutorRequest(autorId));
            return Ok(response);
        }
    }
}