using Travel.Core.Domain.Base;

namespace Travel.Core.Domain
{
    public class Editorial : Entity<int>, IAggregateRoot
    {
        public Editorial()
        { }
        
        public Editorial(string nombre, string sede)
        {
            Nombre = nombre;
            Sede = sede;
        }
        public string Nombre { get; private set; }
        public string Sede { get; private set; }

        public void Update(string nombre, string sede)
        {
            Nombre = nombre;
            Sede = sede;
        }
    }
}