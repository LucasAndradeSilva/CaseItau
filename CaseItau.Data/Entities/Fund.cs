using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CaseItau.Data.Entities
{
    public class Fund
    {     
        [Key]
        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        [StringLength(20, ErrorMessage = "O campo Código não pode ter mais de 20 caracteres.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome não pode ter mais de 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "O campo CNPJ não pode ter mais de 14 caracteres.")]
        public string TaxId { get; set; }

        [Required(ErrorMessage = "O campo Código do Tipo é obrigatório.")]
        public int TypeCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Equity { get; set; }

        [ForeignKey("TypeCode")]
        public Type Type { get; set; }        
    }
}
