using System;
using System.Collections.Generic;
using Travel.Core.Domain.Base;

namespace Travel.Core.Domain
{
    public class Libro : Entity<int>, IAggregateRoot
    {
        public Libro()
        {
        }

        public Libro(string titulo, string sinopsis, string noPaginas, int editorialId)
        {
            Titulo = titulo;
            Sinopsis = sinopsis;
            NoPaginas = noPaginas;
            EditorialId = editorialId;
            Autores = new List<Autor>();
        }

        public string Titulo { get; private set; }
        public string Sinopsis { get; private set; }
        public string  NoPaginas { get; private set; }
        public Editorial Editorial { get; private set; }
        public int EditorialId { get; private set; }
        public List<Autor> Autores { get; set; }

        public void AgregarAutor(Autor autor)
        {
                Autores.Add(autor);
        }

        public void AgregarAutores(List<Autor> autores)
        {
            Autores.AddRange(autores);            
        }

        public void Update(string titulo, string sinopsis, string noPaginas, int editorialId)
        {
            Titulo = titulo;
            Sinopsis = sinopsis;
            NoPaginas = noPaginas;
            EditorialId = editorialId;
        }
    }
}
