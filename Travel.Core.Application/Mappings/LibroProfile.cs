using AutoMapper;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Application.Features.Libros.ViewModels;
using Travel.Core.Domain;

namespace Travel.Core.Application.Mappings
{
    internal class LibroProfile : Profile
    {
        public LibroProfile()
        {
            CreateMap<LibroViewModel, Libro>().ReverseMap();
        }
    }
}