using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblUsuario
    {
        public TblUsuario()
        {
            TblFotoPerfils = new HashSet<TblFotoPerfil>();
            TblPdfFiles = new HashSet<TblPdfFile>();
        }

        public int Id { get; set; }
        public string Usuario { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public long Celular { get; set; }
        public int Idrol { get; set; }
        public int Gender { get; set; }
        public byte[]? PasswordHash { get; set; }
        public Guid? Salt { get; set; }
        public bool? LockOutEnabled { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual TblGender GenderNavigation { get; set; } = null!;
        public virtual TblRole IdrolNavigation { get; set; } = null!;
        public virtual ICollection<TblFotoPerfil> TblFotoPerfils { get; set; }
        public virtual ICollection<TblPdfFile> TblPdfFiles { get; set; }
    }
}
