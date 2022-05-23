using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Application.Features.Editoriales.ViewModels;
using Travel.Core.Application.Features.Libros.ViewModels;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Editoriales.Queries
{
    public class GetLibroByIdQuery : IRequestHandler<GetLibroByIdRequest, GetLibroByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLibroByIdQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<GetLibroByIdResponse> Handle(GetLibroByIdRequest request, CancellationToken cancellationToken)
        {
            var libro = _unitOfWork.LibroRepository.FindWithIncludes(request.LibroId);
            var libroModelView = _mapper.Map<LibroViewModel>(libro);
            return Task.FromResult(new GetLibroByIdResponse(libroModelView));
        }
    }
    public record GetLibroByIdRequest(int LibroId) : IRequest<GetLibroByIdResponse>;

    public record GetLibroByIdResponse(LibroViewModel Libro);
}