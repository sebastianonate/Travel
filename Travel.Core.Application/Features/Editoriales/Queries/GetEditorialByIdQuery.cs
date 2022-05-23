using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Application.Features.Editoriales.ViewModels;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Editoriales.Queries
{
    public class GetEditorialByIdQuery : IRequestHandler<GetEditorialByIdRequest, GetEditorialByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEditorialByIdQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<GetEditorialByIdResponse> Handle(GetEditorialByIdRequest request, CancellationToken cancellationToken)
        {
            var editorial = _unitOfWork.EditorialRepository.Find(request.EditorialId);
            var editorialViewModel = _mapper.Map<EditorialViewModel>(editorial);
            return Task.FromResult(new GetEditorialByIdResponse(editorialViewModel));
        }
    }
    public record GetEditorialByIdRequest(int EditorialId) : IRequest<GetEditorialByIdResponse>;

    public record GetEditorialByIdResponse(EditorialViewModel Editorial);
}