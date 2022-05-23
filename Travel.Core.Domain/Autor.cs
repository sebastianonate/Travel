using System.Collections.Generic;
using Travel.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Travel.Core.Domain
{
    public class Autor : Entity<int>, IAggregateRoot
    {
        public Autor()
        {
        }

        public Autor(string nombre, string apellidos)
        {
            Nombre = nombre;
            Apellidos = apellidos;
        }
        public string Nombre { get; private set; }
        public string Apellidos { get; private set; }
        public List<Libro> Libros { get; private set; }

        public void Update(string nombre, string apellidos)
        {
            Nombre = nombre;
            Apellidos = apellidos;
        }
    }
}