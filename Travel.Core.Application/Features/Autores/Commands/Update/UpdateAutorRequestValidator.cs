using FluentValidation;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;

namespace Travel.Core.Application.Features.Autores.Commands.Update
{
    public class UpdateAutorRequestValidator : AbstractValidator<UpdateAutorRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Autor autor;
        public UpdateAutorRequestValidator()
        {
            RuleFor(t => t.AutorId).Must(ExistirAutor).WithMessage("No existe el autor a actualizar");
            
        }

        private bool ExistirAutor(int id)
        {
            autor = _unitOfWork.AutorRepository.FindFirstOrDefault(t => t.Id == id);
            return autor != null;
        }
    }
}