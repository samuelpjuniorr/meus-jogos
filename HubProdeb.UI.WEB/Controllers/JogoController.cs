using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Service;
using MeusJogos.Domain.Models;
using MeusJogos.Domain.ViewModels;
using MeusJogos.UI.WEB.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeusJogos.UI.WEB.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class JogoController : ControllerBase
    {

        private readonly IJogoService _jogoService;

        public JogoController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }


        /// <summary>
        /// Método POST para cadastrar jogo.
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpPost]
        [Route("cadastrar")]
        public ActionResult<Jogo> Cadastrar(JogoVM model)
        {
            try
            {
                _jogoService.Cadastrar(model);
                return Ok("Jogo cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método get para listar jogos
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpGet]
        [Route("listar")]
        public ActionResult<List<Jogo>> Listar()
        {
            try
            {
                return _jogoService.Listar();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método POST para alterar jogo.
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpPost]
        [Route("alterar")]
        public ActionResult<Jogo> Alterar(Jogo model)
        {
            try
            {
                _jogoService.Alterar(model);
                return Ok("Jogo alterado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método POST para excluir jogo.
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpPost]
        [Route("excluir")]
        public ActionResult<Jogo> Excluir(Jogo model)
        {
            try
            {
                _jogoService.Excluir(model);
                return Ok("Jogo excluido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
