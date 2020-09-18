using MeusJogos.Domain.Models;
using MeusJogos.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeusJogos.UI.WEB.Filters
{
    public class AuthorizationFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //pegando os dados do usuário autenticado
            var t = (ClaimsIdentity)context.HttpContext.User.Identity;
            //pega o id que veio no token jwt
            var id = t.Claims.Where(c => c.Type == ClaimTypes.Sid)
                .Select(c => c.Value).SingleOrDefault();
            
            //pega o nome que veio no token jwt
            var nome = t.Claims.Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value).SingleOrDefault();

            if (nome != null)
            {
                //cria um usuario com o id e email
                Usuario usuario = new Usuario();
                usuario.Id = int.Parse(id);
                usuario.Nome = nome.ToString();


                if (!context.HttpContext.Response.Headers.Keys.Any(x => x == "Authorization"))
                {
                    TokenService ts = new TokenService();
                    //retorna no header da resposta o token
                    context.HttpContext.Response.Headers.Add("Authorization", "Bearer " + ts.GenerateToken(usuario));
                }
            }
        }

    }
}
