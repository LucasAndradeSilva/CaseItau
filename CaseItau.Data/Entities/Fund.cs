using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaseItau.Data.Entities
{
    public class Fund
    {
        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        [StringLength(20, ErrorMessage = "O campo Código deve ter no máximo 20 caracteres.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O campo CNPJ deve ter exatamente 14 dígitos.")]
        public string TaxId { get; set; }

        [Required(ErrorMessage = "O campo Código é obrigatório.")]        
        public int TypeCode { get;  set; }
        public decimal? Equity { get; set; }

        [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
        public Type Type { get; set; }
    }
}
