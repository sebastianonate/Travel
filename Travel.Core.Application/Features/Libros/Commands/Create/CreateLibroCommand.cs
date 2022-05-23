using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Libros.Commands.Create
{
    public class CreateLibroCommand : IRequestHandler<CreateLibroRequest, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLibroCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<int>> Handle(CreateLibroRequest request, CancellationToken cancellationToken)
        {
            var autores = FindAutores(request.Autores);
            if (!autores.Any())
                return Task.FromResult(Result<int>.Fail("El libro debe tener autores"));
            
            var editorial = FindEditorial(request.EditorialId);
            if(editorial == null)
                return Task.FromResult(Result<int>.Fail("El libro debe tener editorial"));
            
            var libro = new Libro(request.Titulo,request.Sinopsis,request.NoPaginas,request.EditorialId);
            
           
            libro.AgregarAutores(autores);
            
            _unitOfWork.LibroRepository.Add(libro);
            _unitOfWork.Commit(cancellationToken);
            return Task.FromResult(Result<int>.Success($"El libro {libro.Id} - {libro.Titulo} fue registrado con éxito"));
        }

        private Editorial FindEditorial(int editorialId)
        {
            return _unitOfWork.EditorialRepository.Find(editorialId);
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

    public record CreateLibroRequest(string Titulo, string Sinopsis, string NoPaginas, int EditorialId, List<AutorIdRequest> Autores) : IRequest<Result<int>>;

    public record AutorIdRequest(int AutorId);
}