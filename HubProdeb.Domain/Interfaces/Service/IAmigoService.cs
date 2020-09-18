using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Interfaces.Service
{
    public interface IAmigoService : IBaseService<Amigo>
    {
        /// <summary>
        /// Cadastrar Amigo
        /// </summary>
        /// <param name="AmigoVM">AmigoVM</param>
        /// <returns></returns>
        void Cadastrar(AmigoVM model);

        /// <summary>
        /// Listar Amigos
        /// </summary>
        /// <returns></returns>
        List<Amigo> Listar();

        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="Amigo">Amigo</param>
        /// <returns></returns>
        void Alterar(Amigo model);

        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="Amigo">Amigo</param>
        /// <returns></returns>
        void Excluir(Amigo model);
    }
}
