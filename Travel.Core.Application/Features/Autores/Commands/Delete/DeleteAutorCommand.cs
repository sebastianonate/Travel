using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Autores.Commands.Delete
{
    public class DeleteAutorCommand : IRequestHandler<DeleteAutorRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAutorCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(DeleteAutorRequest request, CancellationToken cancellationToken)
        {
            var autor = _unitOfWork.AutorRepository.FindAutorWithLibros(request.AutorId);
            if (autor == null)
            {
                return Task.FromResult(Result<int>.Fail("No se encontró el autor"));
            }

            if (autor.Libros.Any())
            {
                return Task.FromResult(Result<int>.Fail("No se puede eliminar un autor con libros asociados"));
            }

            _unitOfWork.AutorRepository.Delete(autor);
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"Autor eliminado correctamente"));
        }
    }

    public record DeleteAutorRequest(int AutorId) : IRequest<Result<int>>;
}