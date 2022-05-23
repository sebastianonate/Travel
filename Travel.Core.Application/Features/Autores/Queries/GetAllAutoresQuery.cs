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
    public class GetAllAutoresQuery : IRequestHandler<GetAllAutoresRequest, GetAllAutoresResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAutoresQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<GetAllAutoresResponse> Handle(GetAllAutoresRequest request, CancellationToken cancellationToken)
        {
            var autores=_unitOfWork.AutorRepository.GetAll().ToList();
            var lista = _mapper.Map<List<AutorViewModel>>(autores);
            return Task.FromResult(new GetAllAutoresResponse(lista));
        }
    }
    public record GetAllAutoresRequest() : IRequest<GetAllAutoresResponse>;

    public record GetAllAutoresResponse(List<AutorViewModel> Autores);
}