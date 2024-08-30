using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb_SPASentirseBien.Models.DTOs
{
    public class RespuestaDTO
    {
        [Key]
        public int RespuestaId { get; set; }

        [Required]
        public string? UsuarioId { get; set; }
        [Required]
        [ForeignKey("UsuarioId")]
        public Usuario? UsuarioClass { get; set; }

        [Required]
        public int PreguntaId { get; set; }
        [Required]
        [ForeignKey("PreguntaId")]
        public Pregunta? PreguntaClass { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage ="La respuesta no puede superar los 150 caracteres.")]
        public string? Descripcion { get; set; }
    }
}