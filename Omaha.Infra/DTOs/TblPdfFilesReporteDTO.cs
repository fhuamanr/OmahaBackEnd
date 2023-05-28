using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omaha.Infra.DTOs
{
    public class TblPdfFilesReporteDTO
    {
        public int Id { get; set; }
        public string TpoFondo { get; set; } = null!;
        public string Periodo { get; set; } = null!;
        public string NombreArchivo { get; set; } = null!;
     
    }
}
