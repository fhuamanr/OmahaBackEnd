using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omaha.Infra.Common
{
    public class UpdateProfile
    {
        public int IdUser { get; set; }
        public string Usuario { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int Gender { get; set; }
    }
}
