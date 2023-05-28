using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblGender
    {
        public TblGender()
        {
            TblUsuarios = new HashSet<TblUsuario>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Vigente { get; set; }

        public virtual ICollection<TblUsuario> TblUsuarios { get; set; }
    }
}
