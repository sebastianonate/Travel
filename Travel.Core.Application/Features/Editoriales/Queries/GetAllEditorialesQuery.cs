using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Application.Features.Editoriales.ViewModels;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Editoriales.Queries
{
    public class GetAllEditorialesQuery : IRequestHandler<GetAllEditorialesRequest, GetAllEditorialesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEditorialesQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<GetAllEditorialesResponse> Handle(GetAllEditorialesRequest request, CancellationToken cancellationToken)
        {
            var editoriales=_unitOfWork.EditorialRepository.GetAll().ToList();
            var lista = _mapper.Map<List<EditorialViewModel>>(editoriales);
            return Task.FromResult(new GetAllEditorialesResponse(lista));
        }
    }
    public record GetAllEditorialesRequest() : IRequest<GetAllEditorialesResponse>;

    public record GetAllEditorialesResponse(List<EditorialViewModel> Editoriales);
}