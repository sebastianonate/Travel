using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Editoriales.Commands.Create
{
    public class CreateEditorialCommand : IRequestHandler<CreateEditorialRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEditorialCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(CreateEditorialRequest request, CancellationToken cancellationToken)
        {
            var editorial = new Domain.Editorial(request.Nombre, request.Sede);
            
            _unitOfWork.EditorialRepository.Add(editorial);
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success("La editorial fue creada con éxito"));
        }
    }

    public record CreateEditorialRequest(string Nombre, string Sede) : IRequest<Result<int>>;

}