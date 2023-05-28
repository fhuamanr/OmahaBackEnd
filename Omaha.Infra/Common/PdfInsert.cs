using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omaha.Infra.Common
{
    public class PdfInsert
    {
        public int IdUsuario { get; set; }
        public string? Periodo { get; set; } = null!;
        public string? Part { get; set; } = null!;
        public string? NombreArchivo { get; set; } = null!;
        public string? File { get; set; } = null;
        private DateTime FechaCarga { get; set; } = DateTime.Now;
        private bool Vigente { get; set; } = true;
    }
}
