
using MeusJogos.Domain.Interfaces.Repository;
using MeusJogos.Domain.Interfaces.Service;
using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using MeusJogos.Infra.CrossCutting.IoC;
using MeusJogos.Infra.Data.Exceptions;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeusJogos.Services.Services
{
    public class JogoService : BaseService<Jogo>, IJogoService
    {

        private readonly IJogoRepository _jogoRepository;
        private readonly IAmigoRepository _amigoRepository;



        public JogoService(IJogoRepository jogoRepositorio, IAmigoRepository amigoRepository)
            : base(jogoRepositorio)
        {

            _jogoRepository = jogoRepositorio;
            _amigoRepository = amigoRepository;

        }

        // <summary>
        /// Cadastrar Jogo
        /// </summary>
        /// <param name="JogoVM">JogoVM</param>
        /// <returns></returns>
        public void Cadastrar(JogoVM model)
        {
            try
            {
                Jogo jogo = new Jogo()
                {
                    Nome = model.Nome,
                    AmigoID = model.AmigoId
                };

                _jogoRepository.Add(jogo);
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
            }


        }


        /// <summary>
        /// Listar Jogos
        /// </summary>
        /// <returns></returns>
        public List<Jogo> Listar()
        {
            try
            {

                return _jogoRepository.Listar();
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
                return new List<Jogo>();
            }
        }

        // <summary>
        /// Alterar Jogo
        /// </summary>
        /// <param name="Jogo">Jogo</param>
        /// <returns></returns>
        public void Alterar(Jogo model)
        {
            try
            {

                _jogoRepository.Update(model);
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
            }


        }

        // <summary>
        /// Excluir Jogo
        /// </summary>
        /// <param name="Jogo">Jogo</param>
        /// <returns></returns>
        public void Excluir(Jogo model)
        {
            try
            {


                _jogoRepository.Remove(model);
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
            }


        }





    }
}
