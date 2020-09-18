
using MeusJogos.Domain.Interfaces.Repository;
using MeusJogos.Domain.Interfaces.Service;
using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using MeusJogos.Infra.CrossCutting.IoC;
using MeusJogos.Infra.Data.Exceptions;
using MeusJogos.Services.Auth;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeusJogos.Services.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;



        public UsuarioService(IUsuarioRepository usuarioRepositorio)
            : base(usuarioRepositorio)
        {

            _usuarioRepository = usuarioRepositorio;

        }

        // <summary>
        /// Cadastrar Usuario
        /// </summary>
        /// <param name="UsuarioVM">UsuarioVM</param>
        /// <returns></returns>
        public void Cadastrar(UsuarioVM model)
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nome = model.Nome,
                    Senha = model.Senha
                };

                _usuarioRepository.Add(usuario);
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
            }


        }

        /// <summary>
        /// Logar 
        /// </summary>
        /// <param name="UsuarioVM">UsuarioVM</param>
        /// <returns></returns>
        public UsuarioVM Login(UsuarioVM model)
        {
            try
            {

                Usuario usu = _usuarioRepository.Login(model);
                if (usu == null)
                    throw new BusinessException("Credenciais inválidas.");

                TokenService ts = new TokenService();
                var token = ts.GenerateToken(usu);

                model.Token = token;

                return model;
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
                return new UsuarioVM();
            }
        }





    }
}
