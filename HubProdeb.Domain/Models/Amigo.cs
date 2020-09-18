
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MeusJogos.Domain.Models
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

    }
}
