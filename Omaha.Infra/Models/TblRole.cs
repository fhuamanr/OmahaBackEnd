using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblPerfiles = new HashSet<TblPerfile>();
            TblUsuarios = new HashSet<TblUsuario>();
        }

        public int Id { get; set; }
        public string NombreRol { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Vigente { get; set; }

        public virtual ICollection<TblPerfile> TblPerfiles { get; set; }
        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
    }
}
