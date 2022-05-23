using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Application.Features.Libros.Commands.Create;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Libros.Commands.Update
{
    public class UpdateLibroCommand : IRequestHandler<UpdateLibroRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateLibroCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(UpdateLibroRequest request, CancellationToken cancellationToken)
        {
            var libro = _unitOfWork.LibroRepository.FindWithIncludes(request.LibroId);
            if (libro == null)
            {
                return Task.FromResult(Result<int>.Fail("No se encontró el libro"));
            }
            
            libro.Update(request.Titulo,request.Sinopsis,request.NoPaginas,request.EditorialId);

            libro.Autores.Clear();
            var autores = FindAutores(request.Autores);
            if(!autores.Any())
                return Task.FromResult(Result<int>.Fail("El libro debe tener autores"));
            libro.AgregarAutores(autores);
            
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"Se actualizó correctamente el libro {libro.Titulo}"));

        }
        private List<Autor> FindAutores(List<AutorIdRequest> autoresFromRequest)
        {
            var autores = new List<Autor>();
            foreach (var autorFromRequest in autoresFromRequest)
            {
                var autor = _unitOfWork.AutorRepository.Find(autorFromRequest.AutorId);
                if(autor != null && !autores.Contains(autor))
                    autores.Add(autor);
            }
            return autores;
        }
    }

    public record UpdateLibroRequest(int LibroId,string Titulo, string Sinopsis, string NoPaginas, int EditorialId, List<AutorIdRequest> Autores) : IRequest<Result<int>>;

}