using AutoMapper;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Application.Features.Editoriales.ViewModels;
using Travel.Core.Domain;

namespace Travel.Core.Application.Mappings
{
    internal class EditorialProfile : Profile
    {
        public EditorialProfile()
        {
            CreateMap<EditorialViewModel, Editorial>().ReverseMap();
        }
    }
}