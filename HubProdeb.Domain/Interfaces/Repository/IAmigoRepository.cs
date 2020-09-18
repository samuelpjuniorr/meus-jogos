using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Repository
{
    public interface IAmigoRepository : IBaseRepository<Amigo>
    {


        /// <summary>
        /// Listar Amigos
        /// </summary>
        /// <returns></returns>
        List<Amigo> Listar();

    }
}
