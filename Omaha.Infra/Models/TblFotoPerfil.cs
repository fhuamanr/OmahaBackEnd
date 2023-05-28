using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblFotoPerfil
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime FechaCarga { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public byte[] ProfilePic { get; set; } = null!;
        public bool Vigente { get; set; }

        public virtual TblUsuario IdUserNavigation { get; set; } = null!;
    }
}
