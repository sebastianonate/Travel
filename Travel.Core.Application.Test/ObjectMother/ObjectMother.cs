using Travel.Core.Domain;

namespace Travel.Core.Application.Test
{
    public static class ObjectMother
    {
        public static Autor CreateAutor()
        {
            return new Autor("Pedro", "Perez");
        }
        public static Editorial CreateEditorial()
        {
            return new Editorial("Editorial10", "Sede5");
        }

        public static Libro CreateLibro(int editorialId)
        {
            return new Libro("Hola Mundo", "Se describe el mundo", "20", editorialId);
        }
    }
}