using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb_SPASentirseBien.Models
{
    public class FormContactoModel
    {
        public string? Email { get; set; }
        public string? Asunto { get; set; }
        public string? Mensaje { get; set; }
    }
}