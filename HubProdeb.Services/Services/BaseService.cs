using MeusJogos.Domain.Interfaces.Repository;
using MeusJogos.Domain.Interfaces.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeusJogos.Services.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;
        bool disposed = false;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Salva o registro no banco e retorna o Id do registro gravado.
        /// </summary>
        /// <param name="obj">Objeto a ser Salvo.</param>
        /// <returns></returns>
        public TEntity Add(TEntity obj)
        {
            return _repository.Add(obj);
        }

        /// <summary>
        /// Obtem O registro de uma determinada entidade a partir do seu Id.
        /// </summary>
        /// <param name="id">Id da Entidade.</param>
        /// <returns></returns>
        public TEntity ObtemPorId(params object[] id)
        {
            return _repository.ObtemPorId(id);
        }

        /// <summary>
        /// Obtem uma lista de todos os registros de uma determinada entidade.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> ObtemTodos()
        {
            return _repository.ObtemTodos();
        }

        /// <summary>
        /// Obtém a Entidade do Repositório Atual.
        /// </summary>
        /// <returns></returns>
        public DbSet<TEntity> Entity()
        {
            return _repository.Entity();
        }

        /// <summary>
        /// Deleta o registro fisicamente no banco de dados.
        /// </summary>
        /// <param name="obj">Objeto a ser excluído.</param>
        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        /// <summary>
        /// Atualiza o registro no banco de dados.
        /// </summary>
        /// <param name="obj">Objeto a ser atualizado.</param>
        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

    }
}
