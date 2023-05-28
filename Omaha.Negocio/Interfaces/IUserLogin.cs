using Omaha.Infra.Common;

namespace Omaha.Negocio.Interfaces
{
    public interface IUserLogin
    {
        Task<AuthenticationModel> LoginUser(LoginUser loginUser);

        Task<string> ActualizaContraseña(ChangePass changePass);


    }
}
