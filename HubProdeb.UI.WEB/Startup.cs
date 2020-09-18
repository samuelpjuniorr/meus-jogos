using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Repository;
using MeusJogos.Domain.Interfaces.Service;
using MeusJogos.Infra.CrossCutting.IoC;
using MeusJogos.Infra.Data.Context;
using MeusJogos.Infra.Data.Repositories;
using MeusJogos.Services.Auth;
using MeusJogos.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeusJogos.UI.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Global.Instance.SetConnectionString(Configuration.GetConnectionString("DefaultConnection"));
            Global.Instance.SetSecret(Configuration.GetValue<string>("Jwt"));




            services.AddDbContext<BDMeusJogosContext>(options => options.UseSqlServer(Global.Instance.GetConnectionString()));

            services.AddCors();

            services.AddMvc();
            services.AddScoped<BDMeusJogosContext, BDMeusJogosContext>();

            //Services
            services.AddScoped<IAmigoService, AmigoService>();
            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();



            //Repositories
            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();


            services.AddControllers();
            services.AddHttpClient();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ///

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
               Configuration.GetSection("TokenConfigurations"))
                   .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            var key = Encoding.ASCII.GetBytes(Global.Instance.GetSecret());
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(key);
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuer = false;
                paramsValidation.ValidateAudience = false;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda � v�lido
                paramsValidation.ValidateLifetime = true;

                // Tempo de toler�ncia para a expira��o de um token (utilizado
                // caso haja problemas de sincronismo de hor�rio entre diferentes
                // computadores envolvidos no processo de comunica��o)
                paramsValidation.ClockSkew = TimeSpan.Zero;

            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("authorization"));
            //inclusão do WithExposedHeaders para o axios renovar token no starter pack

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
