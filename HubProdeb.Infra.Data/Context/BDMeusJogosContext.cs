using MeusJogos.Domain.Models;
using MeusJogos.Infra.Data.Exceptions;
using MeusJogos.Infra.Data.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeusJogos.Infra.Data.Context
{
    public class BDMeusJogosContext : DbContext
    {
        private readonly IHttpContextAccessor _accessor;

        public BDMeusJogosContext(DbContextOptions<BDMeusJogosContext> options) : base(options)
        {

        }
        public BDMeusJogosContext(DbContextOptions<BDMeusJogosContext> options, IHttpContextAccessor accessor) : base(options)
        {
            _accessor = accessor;
        }

        public DbSet<LogMeusJogos> LogHubProdeb { get; set; }
        public DbSet<Amigo> Amigo { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfiguration(new LogMeusJogosMap());
            modelBuilder.ApplyConfiguration(new AmigoMap());
            modelBuilder.ApplyConfiguration(new JogoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());



        }
        public override int SaveChanges()
        {
            try
            {
                var added = new List<KeyValuePair<object, LogMeusJogos>>();

                var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted).ToList();

                foreach (var dbEntityEntry in entries)
                {
                    var entityName = dbEntityEntry.Entity.GetType().Name;

                    var primaryKey = GetPrimaryKeyValue(dbEntityEntry);


                    switch (dbEntityEntry.State)
                    {
                        case EntityState.Added:
                        case EntityState.Deleted:
                            var log = new LogMeusJogos
                            {
                                Tabela = entityName,
                                Acao = dbEntityEntry.State.ToString(),
                                MatriculaUsuario = (_accessor?.HttpContext?.User?.Identity?.Name != null) ? int.Parse(_accessor.HttpContext.User.Identity.Name) : 0,
                                Chaves = primaryKey,
                                NomeColuna = "*",
                                Propriedade = "*",
                                ValorOriginal = String.Empty,
                                ValorAtual = String.Empty,
                                DtcOcorrencia = DateTime.Now
                            };

                            if (dbEntityEntry.State == EntityState.Deleted)
                            {
                                log.ValorOriginal = JsonConvert.SerializeObject(dbEntityEntry.Entity);
                            }

                            Set<LogMeusJogos>().Add(log);

                            if (dbEntityEntry.State != EntityState.Added)
                                continue;

                            var kv = new KeyValuePair<object, LogMeusJogos>(dbEntityEntry.Entity, log);
                            added.Add(kv);
                            break;
                        case EntityState.Modified:
                            GeraLogUpdate(dbEntityEntry, entityName, primaryKey);
                            break;
                    }
                }

                var result = base.SaveChanges();
                UpdateInsertedKey(added);
                return result;

            }
            catch (Exception ex)
            {
                GlobalException.ReturnException(ex);
                return 0;

            }

        }

        /// <summary>
        /// Obt�m uma string com a representa��o das chaves da entidade e seus respectivos valores
        /// </summary>
        /// <param name="entry">Entidade y</param>
        /// <returns></returns>
        private string GetPrimaryKeyValue<T>(T entry)
        {
            var dbEntry = this.Entry(entry);
            var keyValues = GetPrimaryKeyValue(dbEntry);

            return keyValues;
        }


        /// <summary>
        /// Obt�m uma string com a representa��o das chaves da entidade e seus respectivos valores
        /// </summary>
        /// <param name="entry">DbEntityEntry</param>
        /// <returns></returns>
        private string GetPrimaryKeyValue(EntityEntry entry)
        {
            var keyNames = entry.Metadata.FindPrimaryKey().Properties.Select(x => String.Format("{0} : {1}", x.Name, entry.Property(x.Name).CurrentValue)).ToArray();
            var sb = new StringBuilder();
            sb.Append("{");
            for (var i = 0; i < keyNames.Length; i++)
            {
                sb.Append(keyNames[i]);
                sb.Append(i != keyNames.Length - 1 ? "," : "");
            }
            sb.Append("}");
            return sb.ToString();
        }



        /// <summary>
        /// Atualiza o registro das chaves geradas pelo banco
        /// </summary>
        /// <param name="added">Lista com as entrys e as suas respectiva entidades</param>
        private void UpdateInsertedKey(IEnumerable<KeyValuePair<object, LogMeusJogos>> added)
        {
            foreach (var keyvalues in added)
            {
                var log = Set<LogMeusJogos>().Find(keyvalues.Value.Id);

                if (log == null)
                    continue;

                log.Chaves = GetPrimaryKeyValue(keyvalues.Key);
                base.SaveChanges();
            }
        }


        /// <summary>
        /// Gera o objeto de Log com as altera�oes realizadas no update
        /// </summary>
        /// <param name="dbEntityEntry"></param>
        /// <param name="entityName"></param>
        /// <param name="primaryKey"></param>
        private void GeraLogUpdate(EntityEntry dbEntityEntry, string entityName, string primaryKey)
        {
            foreach (var prop in dbEntityEntry.OriginalValues.Properties)
            {
                var originalValue = (dbEntityEntry.OriginalValues[prop] != null
                    ? dbEntityEntry.OriginalValues[prop].ToString()
                    : "");
                var currentValue = (dbEntityEntry.CurrentValues[prop] != null
                    ? dbEntityEntry.CurrentValues[prop].ToString()
                    : "");

                if (originalValue == currentValue)
                    continue;

                var log = new LogMeusJogos
                {
                    Tabela = entityName,
                    Acao = dbEntityEntry.State.ToString(),
                    //Se for uma ação feita por usuário logado ou pelo sistema em background 
                    MatriculaUsuario = (_accessor?.HttpContext?.User?.Identity?.Name != null) ? int.Parse(_accessor.HttpContext.User.Identity.Name) : 0,
                    Chaves = primaryKey,
                    NomeColuna = prop.Name,
                    Propriedade = prop.Name,
                    ValorOriginal = originalValue,
                    ValorAtual = currentValue,
                    DtcOcorrencia = DateTime.Now
                };
                Set<LogMeusJogos>().Add(log);
            }
        }
    }
}
