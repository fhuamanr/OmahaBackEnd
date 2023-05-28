using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Omaha.Infra.Common;
using Omaha.Infra.Context;
using Omaha.Infra.Models;
using Omaha.Negocio.Interfaces;

namespace Omaha.Negocio.Services
{
    public class ProfilePicService : IProfile
    {
        private IConfiguration _config;
        private readonly string _connectionString;
        private readonly OmahaContext _ContextDBSQL;
        private readonly IMapper _mapper;

        public ProfilePicService(IConfiguration config, OmahaContext omahaContext, IMapper mapper)
        {
            _config = config;
            _connectionString = config.GetConnectionString("ConexionDB-EF");
            _ContextDBSQL = omahaContext;
            _mapper = mapper;
        }
        public async Task<ProfilePic> GetProfilePic(int IdUser)
        {
            try
            {
                
                var data = await _ContextDBSQL.TblFotoPerfils
                    .Where(x => x.IdUser == IdUser)
                    .OrderByDescending(x => x.FechaCarga)
                    .FirstOrDefaultAsync();
                if (data is not null)
                {
                    var database = Convert.ToBase64String(data.ProfilePic);
                    var rspnse = new ProfilePic()
                    {
                        NombreArchivo = data.NombreArchivo,
                        Pic = database
                    };
                    return rspnse;
                }
                else
                {
                    return new ProfilePic()
                    {
                        NombreArchivo = "",
                        Pic =""
                    };
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse<string>> InsertProfilePic(InsertProfilePic insertProfilePic)
        {
            var rspta = new ApiResponse<string>();
            try
            {
                byte[] bytes = System.Convert.FromBase64String(insertProfilePic.ProfilePic);

                var nuevoProfile = new TblFotoPerfil()
                {
                    NombreArchivo = insertProfilePic.NombreArchivo,
                    IdUser = insertProfilePic.IdUser,
                    ProfilePic = bytes,
                    FechaCarga = DateTime.Now,
                    Vigente = true
                };
                await _ContextDBSQL.TblFotoPerfils.AddAsync(nuevoProfile);
                await _ContextDBSQL.SaveChangesAsync();
                rspta.Message = "Registro insertado con éxito!";
                rspta.Succeeded = true;
            }
            catch (Exception)
            {

                rspta.Message = "Problemas al insertar la información!";
                rspta.Succeeded = false;
            }
            return rspta;
        }

        public async Task<string> UpdateProfile(UpdateProfile updateProfile)
        {
            try
            {
                var data = await _ContextDBSQL.TblUsuarios
                               .Where(x => x.Id == updateProfile.IdUser)
                               .FirstOrDefaultAsync();

                data.Nombres = updateProfile.Nombres;
                data.Apellidos = updateProfile.Apellidos;
                data.Usuario = updateProfile.Usuario;
                data.Correo = updateProfile.Correo;
                data.Gender = updateProfile.Gender;
                _ContextDBSQL.Update(data);
                await _ContextDBSQL.SaveChangesAsync();
                return "Datos actualizados con exito!";
            }
            catch (Exception)
            {

                return "Problemas al actualizar registro";
            }
           

        }
    }
}
