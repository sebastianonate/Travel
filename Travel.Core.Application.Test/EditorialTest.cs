using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Travel.Core.Application.Features.Editoriales.Commands.Create;
using Travel.Core.Application.Features.Editoriales.Commands.Delete;
using Travel.Core.Application.Features.Editoriales.Commands.Update;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;
using Travel.Infrastructure.Data.Repositories;

namespace Travel.Core.Application.Test
{
    
    public class EditorialTest
    {
        private  IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        [SetUp]
        public void Setup()
        {
            var optionsInMemory = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(@"BancoInmemory")
                .Options;

            _dbContext = new ApplicationDbContext(optionsInMemory);
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        [Test]
        public void CreateEditorialCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new CreateEditorialRequest("Editorial1", "Sede1");
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new CreateEditorialCommand(_unitOfWork).Handle(request, default);
            var editorial=_unitOfWork.EditorialRepository.FindFirstOrDefault(t => t.Nombre == "Editorial1" && t.Sede == "Sede1");

            //ASSERT //AFIRMACION //ENTONCES //THEN
            editorial.Should().NotBeNull();
            hander.Result.Message.Should().StartWith("La editorial fue creada con éxito");
        }
        [Test]
        public void CanUpdateEditorialIfExistCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var editorial = new Editorial("Editorial12", "Sede3");
            _unitOfWork.EditorialRepository.Add(editorial);
            _unitOfWork.Commit(default);
            
            var editorialToUpdate = _unitOfWork.EditorialRepository.FindBy().First();
            var request = new UpdateEditorialRequest(editorialToUpdate.Id,"Editorial4", "Sede4");
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new UpdateEditorialCommand(_unitOfWork).Handle(request, default);
            var editorialAfterUpdate=_unitOfWork.EditorialRepository.Find(editorialToUpdate.Id);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            editorialAfterUpdate.Nombre.Should().Be("Editorial4");
            hander.Result.Message.Should().StartWith("Se actualizó correctamente");
        }
        [Test]
        public void CanNotUpdateEditorialIfNoExistCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new UpdateEditorialRequest(300,"Editorial4", "Sede4");
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new UpdateEditorialCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().StartWith("No se encontró");
        }
        [Test]
        public void CanDeleteEditorialIfExistTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var editorial = new Editorial("Jimena", "Gonzales");
            _unitOfWork.EditorialRepository.Add(editorial);
            _unitOfWork.Commit(default);
            
            var editorialToDelete = _unitOfWork.EditorialRepository.Find(1);
            var request = new DeleteEditorialRequest(editorialToDelete.Id);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new DeleteEditorialCommand(_unitOfWork).Handle(request, default);
            var editorialDeleted = _unitOfWork.AutorRepository.Find(1);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            editorialDeleted.Should().BeNull();
        }
        [Test]
        public void CanNotDeleteEditorialIfNoExistTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new DeleteEditorialRequest(500);

            // ACT // ACCION // CUANDO // WHEN
            var hander = new DeleteEditorialCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().StartWith("No se encontró");
        }
    }
}