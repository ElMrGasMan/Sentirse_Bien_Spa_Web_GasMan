using System.ComponentModel.DataAnnotations;

namespace APIWeb_SPASentirseBien.Models.DTOs.PatchDTOs
{
    public class PreguntaPatchDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage="La pregunta no puede superar los 100 caracteres.")]
        public string? Descripcion { get; set; }
    }
}