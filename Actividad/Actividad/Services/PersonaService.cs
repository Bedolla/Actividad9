using Actividad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Actividad.Services
{
    public static class PersonaService
    {
        private static List<Persona> Personas { get; } = new List<Persona>
        {
            new Persona
            {
                Id = Guid.NewGuid(),
                Nombre = "María Guadalupe Alvarez Negrete",
                Correo = "Lupita@Ucol.mx",
                Telefono = "(312) 000 0001"
            },
            new Persona
            {
                Id = Guid.NewGuid(),
                Nombre = "María Enriqueta Rego Rodriguez",
                Correo = "Enri@Ucol.mx",
                Telefono = "(312) 000 0002"
            },
            new Persona
            {
                Id = Guid.NewGuid(),
                Nombre = "Fernando Bedolla Valencia",
                Correo = "Fer@Ucol.mx",
                Telefono = "(312) 000 0003"
            }
        };

        public static void Crear(Persona persona) =>
            PersonaService.Personas.Add(persona);

        public static List<Persona> Listar() =>
            PersonaService.Personas
                          .OrderBy(p => p.Nombre)
                          .ThenBy(p => p.Correo)
                          .ToList();

        public static void Editar(Persona persona) =>
            PersonaService.Personas.Where(p => p.Id.Equals(persona.Id)).ForEach(p =>
            {
                p.Nombre = persona.Nombre;
                p.Correo = persona.Correo;
                p.Telefono = persona.Telefono;
            });

        public static void Borrar(string id) =>
            PersonaService.Personas
                          .Remove(PersonaService.Personas.FirstOrDefault(p => p.Id == Guid.Parse(id)));

        public static bool NoExiste(Persona persona) =>
            !PersonaService.Personas.Any
            (p =>
                (
                    p.Nombre.Equals(persona.Nombre, StringComparison.CurrentCultureIgnoreCase) ||
                    p.Correo.Equals(persona.Correo, StringComparison.CurrentCultureIgnoreCase)
                )
                &&
                (
                    p.Id != persona.Id
                )
            );
    }
}
