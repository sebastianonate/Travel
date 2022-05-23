using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Libros.Commands.Delete
{
    public class DeleteLibroCommand : IRequestHandler<DeleteLibroRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLibroCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(DeleteLibroRequest request, CancellationToken cancellationToken)
        {
            var libro = _unitOfWork.LibroRepository.Find(request.LibroId);
            if (libro == null)
            {
                return Task.FromResult(Result<int>.Fail("No se encontró el libro"));
            }

            _unitOfWork.LibroRepository.Delete(libro);
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"Libro eliminado correctamente"));
        }
    }

    public record DeleteLibroRequest(int LibroId) : IRequest<Result<int>>;
}