using Ateno.API.ViewObjects;
using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ateno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IControllerService _controllerService;

        public HomeController(IControllerService controllerService)
        {
            _controllerService = controllerService;
        }

        [HttpGet]
        [Route("LoadHome")]
        public async Task<IActionResult> LoadHome()
        {
            try
            {
                HomeDTO data = _controllerService.LoadHome(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (data != null)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Requisição realizada com sucesso!", data));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu uma falha ao carregar dados da Tela Inicial."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, APIResponseVO.Fail("Ocorreu um erro interno ao carregar dados da Tela Inicial."));
            }
        }
    }
}
