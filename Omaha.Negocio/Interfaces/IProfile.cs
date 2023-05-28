using Omaha.Infra.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omaha.Negocio.Interfaces
{
    public interface IProfile
    {
        Task<ProfilePic> GetProfilePic(int IdUser);

        Task<ApiResponse<string>> InsertProfilePic(InsertProfilePic insertProfilePic);

        Task<string> UpdateProfile(UpdateProfile updateProfile);

    }
}
