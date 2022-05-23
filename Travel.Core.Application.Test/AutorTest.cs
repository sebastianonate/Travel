using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Travel.Core.Application.Features.Autores.Commands.Create;
using Travel.Core.Application.Features.Autores.Commands.Delete;
using Travel.Core.Application.Features.Autores.Commands.Update;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;
using Travel.Infrastructure.Data.Repositories;

namespace Travel.Core.Application.Test
{
    public class AutorTests
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
        public void CreateAutorCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new CreateAutorRequest("Juan", "Perez");
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new CreateAutorCommand(_unitOfWork).Handle(request, default);
            var autor=_unitOfWork.AutorRepository.FindBy(t => t.Nombre == "Juan" && t.Apellidos == "Perez");
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            autor.Should().NotBeNull();
            hander.Result.Mensaje.Should().StartWith("El autor fue creado con éxito");
        }
        
        [Test]
        public void CanUpdateAutorIfExistsTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var autor = new Autor("Pedro", "Perez");
            _unitOfWork.AutorRepository.Add(autor);
            _unitOfWork.Commit(default);
            var autorToUpdate = _unitOfWork.AutorRepository.FindFirstOrDefault(t => t.Nombre == "Pedro");
            var request = new UpdateAutorRequest(autorToUpdate.Id,"Lucas", "Perez");
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new UpdateAutorCommand(_unitOfWork).Handle(request, default);
            var autorFromDb = _unitOfWork.AutorRepository.Find(request.AutorId);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            autorFromDb.Nombre.Should().Be("Lucas");
        }
        
        [Test]
        public void CanNotUpdateAutorIfNoExist()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new UpdateAutorRequest(200,"Lucas", "Perez");
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new UpdateAutorCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().StartWith("No se encontró");
        }
        
        [Test]
        public void CanDeleteAutorIfExistTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var autor = new Autor("Jimena", "Gonzales");
            _unitOfWork.AutorRepository.Add(autor);
            _unitOfWork.Commit(default);
            var autorToDelete = _unitOfWork.AutorRepository.Find(1);
            var request = new DeleteAutorRequest(1);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new DeleteAutorCommand(_unitOfWork).Handle(request, default);
            var autorDeleted = _unitOfWork.AutorRepository.Find(1);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            autorDeleted.Should().BeNull();
        }
        
        [Test]
        public void CanNotDeleteAutorIfNoExistTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new DeleteAutorRequest(200);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new DeleteAutorCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().StartWith("No se encontró");
        }

    }
}