using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Service
{
    public interface IJogoService : IBaseService<Jogo>
    {
        /// <summary>
        /// Cadastrar Jogo
        /// </summary>
        /// <param name="JogoVM">JogoVM</param>
        /// <returns></returns>
        void Cadastrar(JogoVM model);

        /// <summary>
        /// Listar Jogos
        /// </summary>
        /// <returns></returns>
        List<Jogo> Listar();

        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="Jogo">Jogo</param>
        /// <returns></returns>
        void Alterar(Jogo model);

        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="Jogo">Jogo</param>
        /// <returns></returns>
        void Excluir(Jogo model);

    }
}
