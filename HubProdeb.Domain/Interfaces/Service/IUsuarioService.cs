using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Service
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        /// <summary>
        /// Cadastrar Usuario
        /// </summary>
        /// <param name="UsuarioVM">UsuarioVM</param>
        /// <returns></returns>
        void Cadastrar(UsuarioVM model);

        /// <summary>
        /// Logar 
        /// </summary>
        /// <param name="UsuarioVM">UsuarioVM</param>
        /// <returns></returns>
        UsuarioVM Login(UsuarioVM model);

    }
}
