using MeusJogos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Infra.Data.Mapping
{
    public class LogMeusJogosMap : IEntityTypeConfiguration<LogMeusJogos>
    {
        public void Configure(EntityTypeBuilder<LogMeusJogos> builder)
        {
            builder.ToTable("LogMeusJogos");

            // Primary Key
            builder.HasKey(t => new { t.Id });

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Tabela)
                .HasColumnName("Tabela");

            builder.Property(c => c.Acao)
                .HasColumnName("Acao");

            builder.Property(c => c.MatriculaUsuario)
                .HasColumnName("MatriculaUsuario");

            builder.Property(c => c.Chaves)
                .HasColumnName("Chaves");

            builder.Property(c => c.NomeColuna)
                .HasColumnName("NomeColuna");

            builder.Property(c => c.Propriedade)
                .HasColumnName("Propriedade");

            builder.Property(c => c.ValorOriginal)
                .HasColumnName("ValorOriginal");

            builder.Property(c => c.ValorAtual)
                .HasColumnName("ValorAtual");

            builder.Property(c => c.DtcOcorrencia)
                .HasColumnName("DtcOcorrencia");
        }
    }
}
