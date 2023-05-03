using System.ComponentModel.DataAnnotations;

namespace Progreso1Proyecto_M.E__F.V.Models
{
    public class Registro_M
    {
        
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public int Semestre { get; set; }

        [Required]
        public string? Materia { get; set; }

        [Required]
        public string? Profesor { get; set; }

        [Required]
        [Range(1, 10)]
        public int Calificacion { get; set; }

        [MaxLength(1000000)]
        public string? Descripcion { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Cualidad { get; set; }

        [Required]
        [Range(1, 3)]
        public int Horario { get; set; }

    }
}
