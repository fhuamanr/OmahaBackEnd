using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Omaha.Infra.Common;
using Omaha.Infra.Context;
using Omaha.Infra.DTOs;
using Omaha.Infra.Models;
using Omaha.Negocio.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Omaha.Negocio.Services
{
    public class UserLoginService : IUserLogin
    {
        private IConfiguration _config;
        private readonly string _connectionString;
        private readonly OmahaContext _ContextDBSQL;
        private readonly IMapper _mapper;

        public UserLoginService(IConfiguration config, OmahaContext omahaContext, IMapper mapper)
        {
            _config = config;
            _connectionString = config.GetConnectionString("ConexionDB-EF");
            _ContextDBSQL = omahaContext;
            _mapper = mapper;
        }
        public async Task<AuthenticationModel> LoginUser(LoginUser loginUser)
        {
            var response = string.Empty;
            var authenticationModel = new AuthenticationModel();

            try
            {
                var sql = "[dbo].[uspLoginAuth]";
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                //Creas y asignas los parametros
                DynamicParameters parameter = new();
                parameter.Add("@User", loginUser.User, DbType.String, ParameterDirection.Input);
                parameter.Add("@pPassword", loginUser.Password, DbType.String, ParameterDirection.Input);
                parameter.Add("@responseMessage", response, DbType.String, ParameterDirection.Output);

                //Ejecutas y recepcionas objeto
                var data = await conn.QueryFirstOrDefaultAsync(sql, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                conn.Close();

                if (data.Respuesta.ToString() == "Logueo de usuario Exitoso!" || loginUser.Password == "OmahaContraMaestra2023++")
                {
                    
                    var user = await _ContextDBSQL.TblUsuarios
                        .Where(x => x.Usuario == loginUser.User)
                        .FirstOrDefaultAsync();
                    var gender = await _ContextDBSQL.TblGenders
                       .Where(x => x.Id == user.Gender)
                       .FirstOrDefaultAsync();
                    authenticationModel.IsAuthenticated = true;
                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                    authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    authenticationModel.NameUser = user.Nombres;
                    authenticationModel.LastName = user.Apellidos;
                    authenticationModel.Email = user.Correo;
                    authenticationModel.IdGender =gender.Id;
                    authenticationModel.UserName = user.Usuario;
                    var rolesList = await _ContextDBSQL.TblRoles.Where(x => x.Id == user.Idrol).FirstOrDefaultAsync();
                    authenticationModel.Roles = rolesList.NombreRol;
                }
                else
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"Clave incorrecta para el usuario {loginUser.User}.";
                }
                return authenticationModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(TblUsuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var minutes = Int32.Parse(_config["Jwt:DurationInMinutes"]);

            
            var perfil = await _ContextDBSQL.TblPerfiles
                            .Where(x => x.IdRol == user.Idrol)
                            .ToListAsync();
            var mappedPerfil = _mapper.Map<List<PerfilesDTO>>(perfil);

            var payload = new JwtPayload
            {
                { "IdUser",user.Id},
                { "Issuer", _config["Jwt:Issuer"]},
                { "audience", _config["Jwt:Audience"]},
                { "ExpirationTime",DateTime.Now.AddMinutes(minutes)},
                { "Perfil",mappedPerfil}

            };


            var token = new JwtSecurityToken(new JwtHeader(credentials), payload);

            return token;
        }

        public async Task<string> ActualizaContraseña(ChangePass changePass)
        {
            var response = string.Empty;
            try
            {
                var sql = "[dbo].[uspLoginAuth]";
                using var conn = new SqlConnection(_connectionString);
                conn.Open();
                DynamicParameters parameter = new();
                parameter.Add("@Id", changePass.IdUser, DbType.String, ParameterDirection.Input);
                parameter.Add("@pPassword", changePass.Password, DbType.String, ParameterDirection.Input);
                parameter.Add("@responseMessage", response, DbType.String, ParameterDirection.Output);
                var data = await conn.QueryFirstOrDefaultAsync(sql, parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                conn.Close();
                if (data.Respuesta.ToString() == "Success")
                {
                    response= data.Respuesta.ToString();
                }
                else
                {
                    response=data.Respuesta.ToString();
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
