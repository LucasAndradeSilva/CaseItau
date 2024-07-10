using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.Data.Entities
{
    public class Fund
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public int TypeCode { get; set; }
        public string TypeName { get; set; }
        public decimal? Equity { get; set; }
    }
}
