using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaseItau.Data.Entities
{
    public class Type
    {
        [Key]
        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public int Code { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(20, ErrorMessage = "O campo Nome não pode ter mais de 20 caracteres.")]
        public string Name { get; set; }
    }
}
