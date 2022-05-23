using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Core.Application.Features.Autores.Commands;
using Travel.Core.Application.Features.Autores.Commands.Create;
using Travel.Core.Application.Features.Autores.Commands.Delete;
using Travel.Core.Application.Features.Autores.Commands.Update;
using Travel.Core.Application.Features.Autores.Queries;
using Travel.Core.Application.Features.Editoriales.Commands.Create;
using Travel.Core.Application.Features.Editoriales.Commands.Delete;
using Travel.Core.Application.Features.Editoriales.Commands.Update;
using Travel.Core.Application.Features.Editoriales.Queries;
using Travel.Core.Application.Features.Libros.Queries;

namespace Travel.Infrastructure.WebApi.Controllers
{
    public class EditorialController : BaseApiController<EditorialController>
    {
        /// <summary>
        /// Metodo para consultar Editoriales
        /// </summary>
        /// <returns>Editoriales</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllEditorialesRequest());
            return Ok(response.Editoriales);
        }
        
        /// <summary>
        /// Metodo para consultar editorial por Id
        /// </summary>
        /// <param name="editorialId">Id de la editorial.</param>
        /// <returns>Editorial</returns>
        [HttpGet("{editorialId}")]
        public async Task<IActionResult> GetById(int editorialId)
        {
            var response = await _mediator.Send(new GetEditorialByIdRequest(editorialId));
            return Ok(response.Editorial);
        }
        
        /// <summary>
        /// Metodo para guardar editorial
        /// </summary>
        /// <param name="request">Datos de la editorial.</param>
        /// <returns>Respuesta</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEditorialRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Metodo para actualizar editorial
        /// </summary>
        /// <param name="editorialId">Id de la editorial.</param>
        /// <param name="request">Datos para actualizar la editorial.</param>
        /// <returns>Respuesta</returns>
        [HttpPut("{editorialId}")]
        public async Task<IActionResult> Put(int editorialId, UpdateEditorialRequest request)
        {
            if (editorialId != request.EditorialId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(request));
        }
        
        /// <summary>
        /// Metodo para eliminar editorial
        /// </summary>
        /// <param name="editorialId">Id de la editorial.</param>
        /// <returns>Respuesta</returns>
        [HttpDelete("{editorialId}")]
        public async Task<IActionResult> Delete(int editorialId)
        {
            var response = await _mediator.Send(new DeleteEditorialRequest(editorialId));
            return Ok(response);
        }
    }
}