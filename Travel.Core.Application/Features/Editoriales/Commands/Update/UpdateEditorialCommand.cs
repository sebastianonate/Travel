using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Editoriales.Commands.Update
{
    public class UpdateEditorialCommand : IRequestHandler<UpdateEditorialRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateEditorialCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(UpdateEditorialRequest request, CancellationToken cancellationToken)
        {
            var editorial = _unitOfWork.EditorialRepository.Find(request.EditorialId);
            if (editorial == null)
            {
                return Task.FromResult(Result<int>.Fail("No se encontró la editorial"));
            }
            
            editorial.Update(request.Nombre,request.Sede);
            
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"Se actualizó correctamente la editorial {editorial.Nombre}"));

        }
    }

    public record UpdateEditorialRequest(int EditorialId,string Nombre, string Sede) : IRequest<Result<int>>;

}