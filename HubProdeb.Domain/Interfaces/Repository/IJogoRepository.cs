using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Repository
{
    public interface IJogoRepository : IBaseRepository<Jogo>
    {
        /// <summary>
        /// Listar Jogos
        /// </summary>
        /// <returns></returns>
        List<Jogo> Listar();

        /// <summary>
        /// Pesquisar Jogos por amigo
        /// </summary>
        /// <returns></returns>
        List<Jogo> PesquisarPorAmigo(int AmigoId);
    }
}
