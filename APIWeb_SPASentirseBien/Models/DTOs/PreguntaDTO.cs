using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb_SPASentirseBien.Models.DTOs
{
    public class PreguntaDTO
    {
        [Key]
        public int PreguntaId { get; set; }
        
        [Required]
        public string? UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly FechaPublicacion { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage="La pregunta no puede superar los 100 caracteres.")]
        public string? Descripcion { get; set; }
    }
}