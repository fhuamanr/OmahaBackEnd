using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblPdfFilesReporte
    {
        public int Id { get; set; }
        public string TpoFondo { get; set; } = null!;
        public string Periodo { get; set; } = null!;
        public string NombreArchivo { get; set; } = null!;
        public byte[]? File { get; set; }
        public DateTime FechaCarga { get; set; }
        public bool Vigente { get; set; }
    }
}
