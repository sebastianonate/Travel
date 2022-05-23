using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Editoriales.Commands.Delete
{
    public class DeleteEditorialCommand : IRequestHandler<DeleteEditorialRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEditorialCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(DeleteEditorialRequest request, CancellationToken cancellationToken)
        {
            var editorial = _unitOfWork.EditorialRepository.Find(request.EditorialId);
            if (editorial == null)
                return Task.FromResult(Result<int>.Fail("No se encontró la editorial"));

            if (HasLibros(editorial.Id))
                return Task.FromResult(Result<int>.Fail("No se pudo eliminar, la editorial tiene libros asociados"));

            _unitOfWork.EditorialRepository.Delete(editorial);
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"Editorial eliminada correctamente"));
        }

        private bool HasLibros(int editorialId)
        {
            var libro = _unitOfWork.LibroRepository.FindFirstOrDefault(t => t.EditorialId == editorialId);
            return libro != null;
        }
    }
    public record DeleteEditorialRequest(int EditorialId) : IRequest<Result<int>>;
}