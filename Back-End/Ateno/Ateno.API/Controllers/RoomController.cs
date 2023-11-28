using Ateno.API.Helpers;
using Ateno.API.ViewObjects;
using Ateno.API.ViewObjects.Room;
using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ateno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IControllerService _controllerService;
        private readonly IStudyDeckService _studyDeckService;
        private readonly IRoomService _roomService;

        public RoomController(IControllerService controllerService, IStudyDeckService studyDeckService, IRoomService roomService)
        {
            _controllerService = controllerService;
            _studyDeckService = studyDeckService;
            _roomService = roomService;
        }

        [HttpGet]
        [Route("LoadRooms")]
        public async Task<IActionResult> LoadRooms()
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                List<RoomDTO> model = _roomService.LoadAllRooms(idUser);
                if (model != null)
                {
                    if(model.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("",model));
                    }

                    return StatusCode(StatusCodes.Status204NoContent, APIResponseVO.Ok("", model));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar salas de estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("LoadRoom")]
        public async Task<IActionResult> LoadRoom([FromQuery] int roomId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                HomeRoomDTO model = _controllerService.LoadRoom(roomId, idUser);
                if (model != null)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", model));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar sala de estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] RoomVO data)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                data.AdminId = idUser;
                ResponseDTO response = await _roomService.Create(new RoomDTO{
                    AdminId = data.AdminId,
                    Code = data.Code,
                    Id = 0,
                    IsPublic = data.IsPublic,
                    Name = data.Name,
                    Users = null
                });
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, APIResponseVO.Ok("Sala de estudo adicionada com sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar salas de estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] RoomVO data)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO response = await _roomService.Edit(new RoomDTO
                {
                    AdminId = data.AdminId,
                    Code = data.Code,
                    Id = data.Id,
                    IsPublic = data.IsPublic,
                    Name = data.Name,
                    Users = null
                }, idUser);

                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, APIResponseVO.Ok("Sala de estudo editada com sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Prepare")]
        public async Task<IActionResult> Prepare([FromQuery] int roomId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                RoomDTO data = _roomService.getRoom(roomId, idUser);
                if (data != null)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", new RoomVO
                    {
                        AdminId = data.AdminId,
                        Name = data.Name,
                        IsPublic = data.IsPublic,
                        Id = data.Id,
                        Code= data.Code,
                    }));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar sala de estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("EnterRoom")]
        public async Task<IActionResult> EnterRoom([FromQuery] string roomCode)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO response = await _roomService.EnterRoom(roomCode, idUser);
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("LoadUsers")]
        public async Task<IActionResult> LoadAllRoomUsers([FromQuery] int roomId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                List<UserDTO> model = _roomService.LoadAllRoomUsers(roomId,idUser);
                if (model != null)
                {
                    if (model.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", model));
                    }

                    return StatusCode(StatusCodes.Status204NoContent, APIResponseVO.Ok("", model));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar usuarios da sala de estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] RoomUserVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO response = await _roomService.AddUser(model.RoomId, model.Email, idUser);
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Usuario vinculado com sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("RemoveUser")]
        public async Task<IActionResult> RemoveUser([FromBody] RoomUserVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO response = await _roomService.RemoveUser(model.RoomId, model.Email, idUser);
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Usuario desvinculado com sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
   
        
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> Remove([FromQuery] int roomId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                ResponseDTO response = await _roomService.Remove(roomId, idUser);
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Sala de Estudo removida com sucesso."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
