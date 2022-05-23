using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Core.Application.Features.Editoriales.Queries;
using Travel.Core.Application.Features.Libros.Commands.Create;
using Travel.Core.Application.Features.Libros.Commands.Delete;
using Travel.Core.Application.Features.Libros.Commands.Update;
using Travel.Core.Application.Features.Libros.Queries;

namespace Travel.Infrastructure.WebApi.Controllers
{
    public class LibroController : BaseApiController<LibroController>
    {
        /// <summary>
        /// Metodo para consultar todos los libros
        /// </summary>
        /// <returns>Libros</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libros = await _mediator.Send(new GetLibrosRequest());
            return Ok(libros.libros);
        }
        
        /// <summary>
        /// Metodo para consultar un libro por Id
        /// </summary>
        /// <param name="libroId">Id del libro.</param>
        /// <returns>Libro</returns>
        [HttpGet("{libroId}")]
        public async Task<IActionResult> GetById(int libroId)
        {
            var response = await _mediator.Send(new GetLibroByIdRequest(libroId));
            return Ok(response.Libro);
        }

        /// <summary>
        /// Metodo para guardar libro
        /// </summary>
        /// <param name="request">Datos del libro.</param>
        /// <returns>Respuesta</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateLibroRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        /// <summary>
        /// Metodo para eliminar libro
        /// </summary>
        /// <param name="libroId">Id del libro.</param>
        /// <returns>Respuesta</returns>
        [HttpDelete("{libroId}")]
        public async Task<IActionResult> Delete(int libroId)
        {
            var response = await _mediator.Send(new DeleteLibroRequest(libroId));
            return Ok(response);
        }
        /// <summary>
        /// Metodo para actualizar libro
        /// </summary>
        /// <param name="libroId">Id del libro.</param>
        /// <param name="request">Datos para actualizar el libro.</param>
        /// <returns>Respuesta</returns>
        [HttpPut("{libroId}")]
        public async Task<IActionResult> Put(int libroId,UpdateLibroRequest request)
        {
            if (libroId != request.LibroId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(request));
        }
    }
}