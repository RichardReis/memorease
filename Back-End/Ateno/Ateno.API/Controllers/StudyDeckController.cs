using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Ateno.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Ateno.WebUI.ViewModels.StudyDeck;
using Ateno.Domain.Enum;
using Ateno.API.ViewObjects;
using Ateno.API.Helpers;
using System.Collections.Generic;
using Ateno.API.ViewObjects.StudyDeck;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ateno.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyDeckController : ControllerBase
    {
        private readonly IStudyProcessService _studyProcessService;
        private readonly IStudyDeckService _studyDeckService;
        private readonly IRoomService _roomService;

        public StudyDeckController(IStudyProcessService studyProcessService, IStudyDeckService studyDeckService, IRoomService roomService)
        {
            _studyProcessService = studyProcessService;
            _studyDeckService = studyDeckService;
            _roomService = roomService;
        }

        [HttpGet]
        [Route("UserDecks")]
        public async Task<IActionResult> UserDecks()
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                List<StudyDeckListDTO> list = _studyProcessService.LoadUserDecks(idUser);

                if(list != null)
                {
                    if(list.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Lista encontrada com sucesso.", list));
                    }

                    return StatusCode(StatusCodes.Status204NoContent, APIResponseVO.Ok("Não a dados para serem listados", list));
                }
               

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Usuario não autenticado."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateStudyDeck([FromBody] StudyDeckVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                if (model.StudyRoomId > 0)
                {
                    bool access = _roomService.IsAdmin(model.StudyRoomId.Value, idUser);
                    if (!access)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Sala não encontrada ou sem permissão de administrador."));
                    }
                }

                if (model.StudyRoomId == -1)
                    model.UserId = idUser;

                ResponseDTO response = await _studyDeckService.Create(new StudyDeckDTO()
                {
                    Id = model.Id,
                    CreatedAt = DateTime.Now,
                    Name = model.Name,
                    StudyRoomId = model.StudyRoomId ?? -1,
                    UserId = model.UserId,
                });

                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Baralho cadastrado com sucesso!!"));
                }

                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Prepare")]
        public async Task<IActionResult> PrepareStudyDeck([FromQuery] int studyDeckId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                Permission check = _studyDeckService.AccessAllowed(studyDeckId, idUser);
                if (check == Permission.Admin)
                {
                    StudyDeckDTO model = _studyDeckService.LoadStudyDeck(studyDeckId, idUser, false);
                    if (model != null)
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", new StudyDeckVO()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            UserId = model.UserId,
                            StudyRoomId = model.StudyRoomId
                        }));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Você não tem permissão para acessar esse baralho."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar baralho de estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> EditStudyDeck([FromQuery] StudyDeckVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                if (model.Id < 1 | !string.IsNullOrWhiteSpace(model.Name))
                {
                    ResponseDTO response = await _studyDeckService.UpdateName(model.Id, model.Name, idUser);
                    if (response.Success)
                    {
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Nome do baralho alterado com sucesso!"));
                    }

                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("O Nome inserido é inválido."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveStudyDeck([FromQuery] int studyDeckId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                if (studyDeckId > 0)
                {
                    ResponseDTO response = await _studyDeckService.RemoveDeck(studyDeckId, idUser);
                    if (response.Success)
                    {
                        string message = "O cartão foi removido deste baralho!";
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok(message));
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Id invalido."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("DeckCards")]
        public async Task<IActionResult> DeckCards([FromQuery] int studyDeckId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                StudyDeckDTO result = _studyDeckService.LoadStudyDeck(studyDeckId, idUser, true);

                if (result.studyCardDTOs != null)
                {
                    if (result.studyCardDTOs.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Lista encontrada com sucesso.", result.studyCardDTOs));
                    }

                    return StatusCode(StatusCodes.Status204NoContent, APIResponseVO.Ok("Não a dados para serem listados", result.studyCardDTOs));
                }


                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Usuario não autenticado."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddCards")]
        public async Task<IActionResult> AddCards([FromBody] StudyDeckCardVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                Permission check = _studyDeckService.AccessAllowed(model.StudyDeckId, idUser);
                if (check != Permission.Admin)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                ICollection<StudyCardDTO> studyCardDTOs = new List<StudyCardDTO>
                {
                    new StudyCardDTO()
                    {
                        StudyDeckId = model.StudyDeckId,
                        Id = model.Id,
                        Back = model.Back,
                        Front = model.Front,
                    }
                };

                ResponseDTO response = await _studyDeckService.AddCards(model.StudyDeckId, studyCardDTOs, idUser);
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Cartas adicionadas com sucesso!!"));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("PrepareCard")]
        public async Task<IActionResult> PrepareStudyCard([FromQuery] int studyCardId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }


                StudyCardDTO model = _studyDeckService.LoadStudyCard(studyCardId, idUser);
                if (model != null)
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", new StudyDeckCardVO()
                    {
                        Id = model.Id,
                        Back = model.Back,
                        Front = model.Front,
                        StudyDeckId = model.StudyDeckId,
                    }));


                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao buscar carta."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("EditCard")]
        public async Task<IActionResult> EditStudyCard([FromQuery] StudyDeckCardVO model)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                if (model.Id < 1)
                {
                    ResponseDTO response = await _studyDeckService.UpdateStudyCard(new StudyCardDTO()
                    {
                        Id = model.Id,
                        Back = model.Back,
                        Front = model.Front,
                        StudyDeckId = model.StudyDeckId,
                    }, idUser);

                    if (response.Success)
                    {
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("Carta alterada com sucesso!"));
                    }

                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Carta inserida é inválida."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("RemoveCard")]
        public async Task<IActionResult> RemoveStudyCard([FromQuery] int studyCardId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                if (studyCardId > 1)
                {
                    ResponseDTO response = await _studyDeckService.RemoveCard(studyCardId, idUser);
                    if (response.Success)
                    {
                        string message = "O cartão foi removido deste baralho!";
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok(message));
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Id invalido."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("LoadStudy")]
        public async Task<IActionResult> LoadStudy([FromQuery] int deckid)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                bool has = _studyProcessService.HasStudy(deckid, idUser);
                if (has)
                {
                    StudyCardDTO studyCardDTO = _studyProcessService.LoadStudy(deckid, idUser);
                    if (studyCardDTO != null)
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", studyCardDTO));
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, APIResponseVO.Ok("Não há estudos para esse baralho."));
                }

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao iniciar estudo."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("SaveStudy")]
        public async Task<IActionResult> SaveStudy([FromQuery] int id, int answer)
        {
            try
            {
                try
                {
                    string idUser = UserAuth.GetUserAuth(Request);
                    if (idUser == null || string.IsNullOrEmpty(idUser))
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                    }

                    ResponseDTO response = await _studyProcessService.SaveStudy(id, answer, idUser);
                    if (response.Success)
                        return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok(""));

                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail(response.Message));
                }
                catch
                {
                    return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao prosseguir estudo, tente novamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }      

        [HttpGet]
        [Route("DeckInfo")]
        public async Task<IActionResult> DeckInfo([FromQuery] int deckId)
        {
            try
            {
                string idUser = UserAuth.GetUserAuth(Request);
                if (idUser == null || string.IsNullOrEmpty(idUser))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, APIResponseVO.Fail("Usuario não autenticado."));
                }

                DeckInfoDTO DeckInfo = await _studyDeckService.DeckInfo(deckId, idUser);
                if (DeckInfo != null)
                    return StatusCode(StatusCodes.Status200OK, APIResponseVO.Ok("", DeckInfo));

                return StatusCode(StatusCodes.Status400BadRequest, APIResponseVO.Fail("Ocorreu um erro ao calcular desempenho do baralho."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
