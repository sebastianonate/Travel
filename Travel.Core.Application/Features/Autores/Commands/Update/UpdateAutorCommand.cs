using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using FluentValidation;
using MediatR;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Autores.Commands.Update
{
    public class UpdateAutorCommand : IRequestHandler<UpdateAutorRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAutorCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(UpdateAutorRequest request, CancellationToken cancellationToken)
        {
            var autor = _unitOfWork.AutorRepository.Find(request.AutorId);
            if (autor == null)
            {
                return Task.FromResult(Result<int>.Fail("No se encontró el autor"));
            }
            
            autor.Update(request.Nombre,request.Apellidos);
            
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"Se actualizó correctamente el autor {autor.Nombre} {autor.Apellidos}"));

        }
    }

    public record UpdateAutorRequest(int AutorId,string Nombre, string Apellidos) : IRequest<Result<int>>;

}