using GunShopBackPart.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace GunShopBackPart.Tool.JVT
{
    public enum Role
    {
        Admin,
        User
    }
    public class JVTProvider: IJVTProvider
    {
        private readonly IOptions<JvtOptions> _settings;

        public JVTProvider(IOptions<JvtOptions> settings)
        {
            _settings = settings;
        }

        public string GenJVT(int id, string username, Role role) 
        {
            Claim[] claims = new Claim[]
            {
                new Claim("id", id.ToString()),
                new Claim("username", username),
                new Claim("role", role.ToString())
            };          
            var singningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_settings.Value.SecretKey)),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256
            );

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(_settings.Value.ExpireMinutes),
                signingCredentials: singningCredentials,
                claims: claims
            );

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
            return tokenHandler;
        }
    }
}
