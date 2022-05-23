using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Autores.Commands.Create
{
    public class CreateAutorCommand : IRequestHandler<CreateAutorRequest, CreateAutorResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAutorCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<CreateAutorResponse> Handle(CreateAutorRequest request, CancellationToken cancellationToken)
        {
            var autor = new Autor(request.Nombre, request.Apellidos);
            _unitOfWork.AutorRepository.Add(autor);
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(new CreateAutorResponse("El autor fue creado con éxito"));
        }
    }

    public record CreateAutorRequest(string Nombre, string Apellidos) : IRequest<CreateAutorResponse>;

    public record CreateAutorResponse(string Mensaje);
}