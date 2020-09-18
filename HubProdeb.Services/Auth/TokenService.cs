using MeusJogos.Domain.Models;
using MeusJogos.Infra.CrossCutting.IoC;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace MeusJogos.Services.Auth
{
    public class TokenService
    {
        public object GenerateToken(Usuario user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Id.ToString(), "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Nome ),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                }
            );

            var key = Encoding.ASCII.GetBytes(Global.Instance.GetSecret());

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao.AddMinutes(15);

            var handler = new JwtSecurityTokenHandler();

            TokenConfigurations tokenConfigurations = new TokenConfigurations();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            return handler.WriteToken(securityToken);
        }
    }
}
