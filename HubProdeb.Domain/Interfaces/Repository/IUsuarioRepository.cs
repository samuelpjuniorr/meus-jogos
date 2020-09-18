using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {

        /// <summary>
        /// Logar 
        /// </summary>
        /// <param name="UsuarioVM">UsuarioVM</param>
        /// <returns></returns>
        Usuario Login(UsuarioVM model);
        
    }
}
