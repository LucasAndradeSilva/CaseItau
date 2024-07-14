using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.Domain.DTO
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Body { get; set; }
    }
}
