using System.ComponentModel.DataAnnotations;

namespace APIWeb_SPASentirseBien.Models
{
    public class Noticia
    {
        [Key]
        public int NoticiaId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="El titulo de la noticia no puede exceder 50 caracteres.")]
        public string? Titulo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly FechaPublicacion { get; set; }
        public string? RutaImagen { get; set; }
        [Required]
        public string? RutaPDF { get; set; }
    }
}