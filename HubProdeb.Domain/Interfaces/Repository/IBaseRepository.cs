using MeusJogos.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IBaseService<TEntity> where TEntity : class
    {

    }
}
