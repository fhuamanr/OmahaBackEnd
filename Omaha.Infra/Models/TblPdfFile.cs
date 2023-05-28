using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblPdfFile
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Periodo { get; set; } = null!;
        public string Part { get; set; } = null!;
        public string NombreArchivo { get; set; } = null!;
        public byte[]? File { get; set; }
        public DateTime FechaCarga { get; set; }
        public bool Vigente { get; set; }

        public virtual TblUsuario IdUsuarioNavigation { get; set; } = null!;
    }
}
