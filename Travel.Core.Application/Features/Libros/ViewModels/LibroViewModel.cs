using System.Collections.Generic;
using Travel.Core.Application.Features.Autores.ViewModels;
using Travel.Core.Application.Features.Editoriales.ViewModels;

namespace Travel.Core.Application.Features.Libros.ViewModels
{
    public record LibroViewModel()
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string  NoPaginas { get; set; }
        public List<AutorViewModel> Autores { get; set; }
        public EditorialViewModel Editorial { get; set; }
    }
}