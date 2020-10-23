using Actividad.Models;
using Actividad.Services;
using System.Collections.Generic;

namespace Actividad.ViewModels
{
    public class PersonasViewModel
    {
        public List<Persona> Personas { get; set; } = PersonaService.Listar();
    }
}
