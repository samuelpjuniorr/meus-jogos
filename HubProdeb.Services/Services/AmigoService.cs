
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
    public class AmigoService : BaseService<Amigo>, IAmigoService
    {

        private readonly IAmigoRepository _amigoRepository;
        private readonly IJogoRepository _jogoRepository;



        public AmigoService(IAmigoRepository amigoRepositorio, IJogoRepository jogoRepository)
            : base(amigoRepositorio)
        {

            _amigoRepository = amigoRepositorio;
            _jogoRepository = jogoRepository;

        }

        // <summary>
        /// Cadastrar Amigo
        /// </summary>
        /// <param name="AmigoVM">AmigoVM</param>
        /// <returns></returns>
        public void Cadastrar(AmigoVM model)
        {
            try
            {
                Amigo amigo = new Amigo()
                {
                    Nome = model.Nome
                };

                _amigoRepository.Add(amigo);
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
        public List<Amigo> Listar()
        {

            try
            {

                return _amigoRepository.Listar();
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
                return new List<Amigo>();
            }

        }

        // <summary>
        /// Alterar Amigo
        /// </summary>
        /// <param name="Amigo">Amigo</param>
        /// <returns></returns>
        public void Alterar(Amigo model)
        {
            try
            {

                _amigoRepository.Update(model);
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
            }


        }

        // <summary>
        /// Excluir Amigo
        /// </summary>
        /// <param name="Amigo">Amigo</param>
        /// <returns></returns>
        public void Excluir(Amigo model)
        {
            try
            {
                List<Jogo> listaJogos = _jogoRepository.PesquisarPorAmigo(model.Id);

                if (listaJogos.Any())
                    throw new BusinessException("Existem jogos empretados para esse amigo");

                _amigoRepository.Remove(model);
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
            }


        }





    }
}
