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
    public class AmigoRepository : BaseRepository<Amigo>, IAmigoRepository
    {
        public AmigoRepository(BDMeusJogosContext Db) : base(Db)
        {
            _Db = Db;
        }



        /// <summary>
        /// Listar Amigos
        /// </summary>
        /// <returns></returns>
        public List<Amigo> Listar()
        {
            List<Amigo> amigos = _Db.Amigo.ToList();


            return amigos;
        }


    }
}
