using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omaha.Infra.DTOs
{
    public class TblPdfFileDTO
    {
        public int Id { get; set; }      
        public string? Periodo { get; set; } 
        public string? Part { get; set; } 
        public string? NombreArchivo { get; set; } 
    }
}
