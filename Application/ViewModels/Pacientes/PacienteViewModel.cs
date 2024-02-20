namespace MedicSystem.Core.Application.ViewModels.Pacientes
{
    public class PacienteViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto => $"{Apellido}, {Nombre}";
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string FechaNacimiento { get; set; }
        public bool Fumador { get; set; }
        public bool Alergias { get; set; }
        public string? Foto { get; set; }

        public List<string>? Pacientes { get; set; }
        public List<string>? Citas { get; set; }
    }
}
