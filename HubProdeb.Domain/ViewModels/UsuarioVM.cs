
using MeusJogos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.ViewModels
{
    public class UsuarioVM
    {

        public string Nome { get; set; }
        public string Senha { get; set; }
        public Object? Token { get; set; }


    }

}
