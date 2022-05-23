using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Autores.Queries
{
    public class GetAutorByIdQuery : IRequestHandler<GetAutorByIdRequest, GetAutorByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAutorByIdQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<GetAutorByIdResponse> Handle(GetAutorByIdRequest request, CancellationToken cancellationToken)
        {
            var autor = _unitOfWork.AutorRepository.Find(request.AutorId);
            var autorViewModel = _mapper.Map<AutorViewModel>(autor);
            return Task.FromResult(new GetAutorByIdResponse(autorViewModel));
        }
    }
    public record GetAutorByIdRequest(int AutorId) : IRequest<GetAutorByIdResponse>;

    public record GetAutorByIdResponse(AutorViewModel Autor);
}