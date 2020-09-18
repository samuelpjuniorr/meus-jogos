using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Service;
using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeusJogos.UI.WEB.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        /// <summary>
        /// Método POST para cadastrar usuario.
        /// </summary>
        [HttpPost]
        [Route("cadastrar")]
        public ActionResult<Usuario> Cadastrar(UsuarioVM model)
        {
            try
            {
                _usuarioService.Cadastrar(model);
                return Ok("Usuario cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método POST para logar.
        /// </summary>
        [HttpPost]
        [Route("login")]
        public ActionResult<UsuarioVM> Login(UsuarioVM model)
        {
            try
           {
                return Ok(_usuarioService.Login(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
