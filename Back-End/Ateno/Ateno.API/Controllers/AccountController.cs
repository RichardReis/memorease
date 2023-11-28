using Ateno.API.Helpers;
using Ateno.API.ViewObjects;
using Ateno.API.ViewObjects.Account;
using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ateno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVO model)
        {
            try
            {
                ResponseObjectDTO<TokenUserDTO> result = await _userService.Authentication(model.Email, model.Password);
                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status202Accepted, APIResponseVO.Ok("Usuario Autenticado.", result.Object));
                }

                return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVO model)
        {
            try
            {
                ResponseDTO result = await _userService.Register(new UserDTO{
                        Id = null,
                        Email = model.Email,
                        FirstName = null,
                        Name = model.Name,
                    }, 
                    model.Password
                );

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, APIResponseVO.Ok("Usuario Cadastradado com Sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("UserInfo")]
        public async Task<IActionResult> UserInfo()
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if(idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                UserDTO result = _userService.GetById(idUser);

                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Usuario Cadastradado com Sucesso.", result));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Não soi possivel recuperar dados do Usuario."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ChangeData")]
        public async Task<IActionResult> ChangeData([FromBody] ChangeDataVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO result = await _userService.Update(model.Name, model.Email, idUser);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Usuario Atualizado com Sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO result = await _userService.Delete(idUser);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Usuario Desativado com Sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
