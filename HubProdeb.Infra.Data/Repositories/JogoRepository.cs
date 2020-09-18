using MeusJogos.Domain.Interfaces.Repository;
using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using MeusJogos.Infra.Data.Context;
using MeusJogos.Infra.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeusJogos.Infra.Data.Repositories
{
    public class JogoRepository : BaseRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(BDMeusJogosContext Db) : base(Db)
        {
            _Db = Db;
        }

        /// <summary>
        /// Listar Jogos
        /// </summary>
        /// <returns></returns>
        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _Db.Jogo.Include(x=>x.Amigo).ToList();

            return jogos;
        }


        /// <summary>
        /// Pesquisar Jogos por amigo
        /// </summary>
        /// <returns></returns>
        public List<Jogo> PesquisarPorAmigo(int AmigoId)
        {
            return _Db.Jogo.Where(x => x.AmigoID == AmigoId).ToList();
        }

    }
}
