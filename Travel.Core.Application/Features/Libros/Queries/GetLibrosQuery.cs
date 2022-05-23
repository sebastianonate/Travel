using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Travel.Core.Application.Features.Libros.ViewModels;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Libros.Queries
{
    public class GetLibrosQuery : IRequestHandler<GetLibrosRequest, GetLibrosResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLibrosQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<GetLibrosResponse> Handle(GetLibrosRequest request, CancellationToken cancellationToken)
        {
            
            var libros=_unitOfWork.LibroRepository.GetAll();
            var librosViewModel = new List<LibroViewModel>();

            _mapper.Map(libros, librosViewModel);

            return Task.FromResult(new GetLibrosResponse(librosViewModel));
        }
    }

    public record GetLibrosRequest() : IRequest<GetLibrosResponse>;

    public record GetLibrosResponse(List<LibroViewModel> libros);

    
}