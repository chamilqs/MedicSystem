namespace MedicSystem.Core.Application.ViewModels.Medicos
{
    public class MedicoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto => $"{Apellido}, {Nombre}";
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Foto { get; set; }

    }
}
