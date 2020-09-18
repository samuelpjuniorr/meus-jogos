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
    [Route("api/amigo")]
    public class AmigoController : ControllerBase
    {

        private readonly IAmigoService _amigoService;

        public AmigoController(IAmigoService amigoService)
        {
            _amigoService = amigoService;
        }


        /// <summary>
        /// Método POST para cadastrar amigo.
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpPost]
        [Route("cadastrar")]
        public ActionResult<Amigo> Cadastrar(AmigoVM model)
        {
            try
            {
                _amigoService.Cadastrar(model);
                return Ok("Amigo cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método get para listar amigos
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpGet]
        [Route("listar")]
        public ActionResult<List<Amigo>> Listar()
        {
            try
            {
                return _amigoService.Listar();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Método POST para alterar amigo.
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpPost]
        [Route("alterar")]
        public ActionResult<Amigo> Alterar(Amigo model)
        {
            try
            {
                _amigoService.Alterar(model);
                return Ok("Amigo alterado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método POST para excluir amigo.
        /// </summary>
        [Authorize]
        [AuthorizationFilterAttribute]
        [HttpPost]
        [Route("excluir")]
        public ActionResult<Amigo> Excluir(Amigo model)
        {
            try
            {
                _amigoService.Excluir(model);
                return Ok("Amigo excluido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
