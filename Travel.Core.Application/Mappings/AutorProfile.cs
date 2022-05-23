using AutoMapper;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Domain;

namespace Travel.Core.Application.Mappings
{
    internal class AutorProfile : Profile
    {
        public AutorProfile()
        {
            CreateMap<AutorViewModel, Autor>().ReverseMap();
        }
    }
}