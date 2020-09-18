using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Domain.Models
{
    public class LogMeusJogos
    {
        public int Id { get; set; }
        public string Tabela { get; set; }
        public string Acao { get; set; }
        public int MatriculaUsuario { get; set; }
        public string Chaves { get; set; }
        public string NomeColuna { get; set; }
        public string Propriedade { get; set; }
        public string ValorOriginal { get; set; }
        public string ValorAtual { get; set; }
        public DateTime DtcOcorrencia { get; set; }
    }
}
