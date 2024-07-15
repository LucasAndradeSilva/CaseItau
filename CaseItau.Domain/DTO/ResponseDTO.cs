using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.Domain.DTO
{
    public class ResponseDTO
    {
        public ResponseDTO(dynamic body, bool success = true, string message = "Sucesso!")
        {            
            Success = success;
            Message = message;
            Body = body;                     
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Body { get; set; }
    }
}
