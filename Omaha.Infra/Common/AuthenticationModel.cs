namespace Omaha.Infra.Common
{
    public class AuthenticationModel
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public string? NameUser { get; set; }
        public string? LastName { get; set; }
        public int IdGender { get; set; }
        public string? Email { get; set; }
        public string? Roles { get; set; }
        public string? Token { get; set; }

    }
}
