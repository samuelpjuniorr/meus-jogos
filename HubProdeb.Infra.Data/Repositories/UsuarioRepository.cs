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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(BDMeusJogosContext Db) : base(Db)
        {
            _Db = Db;
        }

        /// <summary>
        /// Logar 
        /// </summary>
        /// <param name="UsuarioVM">UsuarioVM</param>
        /// <returns></returns>
        public Usuario Login(UsuarioVM model)
        {
            try
            {
                return _Db.Usuario.FirstOrDefault(x => x.Nome.ToUpper().Trim() == model.Nome.ToUpper().Trim() && x.Senha.ToUpper().Trim() == model.Senha.ToUpper().Trim());
                
            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
                return new Usuario();
            }
        }

    }
}
