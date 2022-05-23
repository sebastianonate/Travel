using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Travel.Core.Application.Features.Editoriales.Commands.Create;
using Travel.Core.Application.Features.Editoriales.Commands.Delete;
using Travel.Core.Application.Features.Editoriales.Commands.Update;
using Travel.Core.Application.Features.Libros.Commands.Create;
using Travel.Core.Application.Features.Libros.Commands.Delete;
using Travel.Core.Application.Features.Libros.Commands.Update;
using Travel.Core.Domain;
using Travel.Core.Domain.Contracts;
using Travel.Infrastructure.Data.Context;
using Travel.Infrastructure.Data.Repositories;

namespace Travel.Core.Application.Test
{
    
    public class LibroTest
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
            
            _unitOfWork.AutorRepository.Add(ObjectMother.CreateAutor());
            _unitOfWork.EditorialRepository.Add(ObjectMother.CreateEditorial());
            _unitOfWork.Commit(default);
            
        }

        [Test]
        public void CreateLibroCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var listAutores = new List<AutorIdRequest>();
            var autor = _unitOfWork.AutorRepository.FindBy().First();
            listAutores.Add(new AutorIdRequest(autor.Id));
            var editorialId = _unitOfWork.EditorialRepository.FindBy().First().Id;
            var request = new CreateLibroRequest("El mundo","Describe el mundo","20",editorialId,listAutores);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new CreateLibroCommand(_unitOfWork).Handle(request, default);
            var libroCreate=_unitOfWork.LibroRepository.FindFirstOrDefault(t => t.Titulo == "El mundo");

            //ASSERT //AFIRMACION //ENTONCES //THEN
            libroCreate.Should().NotBeNull();
            hander.Result.Message.Should().Contain("fue registrado con éxito");
        }
        [Test]
        public void CanUpdateLibroIfExistCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var editorialId = _unitOfWork.EditorialRepository.FindBy().First().Id;
            var autor= _unitOfWork.AutorRepository.FindBy().First();
            var libro = ObjectMother.CreateLibro(editorialId);

            libro.AgregarAutor(autor);

            _unitOfWork.LibroRepository.Add(libro);
            _unitOfWork.Commit(default);

            var autores = new List<AutorIdRequest>(); 
            autores.Add(new AutorIdRequest(autor.Id));

            var libroToUpdate = _unitOfWork.LibroRepository.FindBy().First();
            var request = new UpdateLibroRequest(libroToUpdate.Id,"Libro1", "Descripción libro 1", "29",editorialId, autores);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new UpdateLibroCommand(_unitOfWork).Handle(request, default);
            var libroAfterUpdate=_unitOfWork.LibroRepository.Find(libroToUpdate.Id);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            libroAfterUpdate.Titulo.Should().Be("Libro1");
            hander.Result.Message.Should().StartWith("Se actualizó correctamente");
        }
        [Test]
        public void CanNotUpdateLibroIfNoExistCommandTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new UpdateLibroRequest(300,"Libro1", "Se describe","2",1,null);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new UpdateLibroCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().StartWith("No se encontró");
        }
        [Test]
        public void CanDeleteLibroIfExistTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var editorialId = _unitOfWork.EditorialRepository.FindBy().First().Id;
            var libro = ObjectMother.CreateLibro(editorialId);
            var autor= _unitOfWork.AutorRepository.FindBy().First();
            libro.AgregarAutor(autor);

            _unitOfWork.LibroRepository.Add(libro);
            _unitOfWork.Commit(default);
            
            var libroToDelete = _unitOfWork.LibroRepository.FindBy().First();
            var request = new DeleteLibroRequest(libroToDelete.Id);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new DeleteLibroCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().Be("Libro eliminado correctamente");
            var libroDeleted = _unitOfWork.LibroRepository.FindFirstOrDefault(t=>t.Id==libroToDelete.Id);
            libroDeleted.Should().BeNull();
        }
        
        [Test]
        public void CanNotDeleteLibroIfNoExistTest()
        {
            //ARRANGE //PREPARAR // DADO // GIVEN
            var request = new DeleteLibroRequest(500);
            
            // ACT // ACCION // CUANDO // WHEN
            var hander = new DeleteLibroCommand(_unitOfWork).Handle(request, default);
            
            //ASSERT //AFIRMACION //ENTONCES //THEN
            hander.Result.Message.Should().StartWith("No se encontró");
        }
    }
}