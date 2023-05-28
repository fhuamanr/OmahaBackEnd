using System;
using System.Collections.Generic;

namespace Omaha.Infra.Models
{
    public partial class TblPerfile
    {
        public int Id { get; set; }
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;
        public int IdRol { get; set; }
        public bool Estado { get; set; }

        public virtual TblRole IdRolNavigation { get; set; } = null!;
    }
}
