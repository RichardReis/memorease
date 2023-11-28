using Ateno.Application.DTOs;
using Ateno.Application.Interfaces;
using Ateno.Domain.Account;
using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ateno.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        

        public UserService(IAccountService accountService, IUserRepository userRepository, IMapper mapper)
        {
            _accountService = accountService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseObjectDTO<TokenUserDTO>> Authentication(string userName, string password)
        {
            try
            {
                ResponseObjectDTO<TokenUserDTO> response = new ResponseObjectDTO<TokenUserDTO>();
                response.Success = _userRepository.CheckBlockedAccount(userName);
                if(response.Success)
                    return new ResponseObjectDTO<TokenUserDTO>() { Message = "Usuário bloqueado." };
                response.Success = await _accountService.Authenticate(userName, password);
                if (!response.Success)
                    response.Message = "Usuário/Senha inválidos.";

                response.Object = await CreateTokenJwt(userName);
                return response;
            }
            catch (Exception)
            {
                return new ResponseObjectDTO<TokenUserDTO>() { Message = "Usuário/Senha inválidos." };
            }
        }

        private async Task<TokenUserDTO> CreateTokenJwt(string userName)
        {
            try
            {
                int configTime = 60;

                User user = _userRepository.GetByEmail(userName);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("name", user.Name),
                    new Claim("email", user.Email)
                };

                SymmetricSecurityKey authSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ateno Secret Token JWT For Auth The User In Application, This Key Is Unique"));

                SigningCredentials credentials =
                    new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512);

                string issuer = "Emissor";
                string audience = "Publico";

                JwtSecurityToken token = new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        expires: DateTime.Now.AddMinutes(configTime),
                        claims: authClaims,
                        signingCredentials: credentials
                    );

                string refresh = await _accountService.GenerateRefreshTokenUser(userName);

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                TokenUserDTO tokenVO = new TokenUserDTO
                {
                    Value = tokenString,
                    RefreshToken = refresh,
                    Expiration = token.ValidTo.AddHours(-3)
                };

                return tokenVO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserDTO GetById(string id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetFirstName(string id)
        {
            try
            {
                return _userRepository.GetFirstName(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseDTO> Register(UserDTO userDTO, string password)
        {
            try
            {
                if (userDTO == null || string.IsNullOrWhiteSpace(userDTO.Name))
                    return new ResponseDTO() { Message = "O Nome é obrigatório." };
                if (string.IsNullOrWhiteSpace(userDTO.Email))
                    return new ResponseDTO() { Message = "O Email é obrigatório." };
                userDTO.Email = userDTO.Email.ToLower();
                userDTO.Id = await _accountService.RegisterUser(userDTO.Email, password);
                switch (userDTO.Id)
                {
                    case "DuplicateUserName":
                        return new ResponseDTO() { Message = "Usuário já cadastrado." };

                    case "PasswordMismatch":
                        return new ResponseDTO() { Message = "Senha incorreta." };

                    case "PasswordTooShort":
                        return new ResponseDTO() { Message = "A senha precisa ter pelo menos 6 caracteres." };

                    case "PasswordRequiresUniqueChars":
                        return new ResponseDTO() { Message = "A senha precisa ter pelo menos três caracteres diferentes." };

                    case "PasswordRequiresDigit":
                        return new ResponseDTO() { Message = "A senha precisa conter um número" };

                    default:
                        break;
                }
                if (string.IsNullOrEmpty(userDTO.Id))
                    return new ResponseDTO() { Message = "A senha informada é muito fraca." };
                User user = _mapper.Map<User>(userDTO);
                ResponseDTO response = new ResponseDTO();
                response.Success = await _userRepository.Create(user);
                if (!response.Success)
                    response.Message = "Falha ao cadastrar informações do usuário.";
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao cadastrar informações do usuário." };
            }
        }

        public async Task<ResponseDTO> Update(string name, string email, string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || name.Length < 4)
                    return new ResponseDTO() { Message = "O Nome é obrigatório." };
                if (string.IsNullOrWhiteSpace(email) || email.Length < 10)
                    return new ResponseDTO() { Message = "Email inválido ou não inserido." };
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Usuário inválido" };
                User user = _userRepository.GetById(userId);
                if(user == null)
                    return new ResponseDTO() { Message = "Falha ao alterar dados." };
                user.Update(name, email);
                ResponseDTO response = new ResponseDTO();
                response.Success = await _accountService.ChangeEmail(user.Id, email);
                if(!response.Success)
                    return new ResponseDTO() { Message = "Falha ao alterar dados." };
                response.Success = await _userRepository.Update(user);
                if(!response.Success)
                    return new ResponseDTO() { Message = "Falha ao alterar dados." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao alterar dados." };
            }
        }

        public async Task<ResponseDTO> Delete(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Usuário inválido" };

                User user = _userRepository.GetById(userId);

                if (user == null)
                    return new ResponseDTO() { Message = "Falha ao deletar usuario." };

                user.Disable();
                ResponseDTO response = new ResponseDTO();
                response.Success = await _userRepository.Update(user);
                if (!response.Success)
                    return new ResponseDTO() { Message = "Falha ao deletar usuario." };
                return response;
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao deletar usuario." };
            }
        }

        public async Task<ResponseDTO> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return new ResponseDTO() { Message = "Usuário inválido" };
                if (string.IsNullOrWhiteSpace(currentPassword))
                    return new ResponseDTO() { Message = "A Senha antiga é inválida." };
                if (string.IsNullOrWhiteSpace(newPassword))
                    return new ResponseDTO() { Message = "A nova Senha é inválida." };
                string result = await _accountService.ChangePassword(userId, currentPassword, newPassword);
                switch (result)
                {
                    case "OK":
                        return new ResponseDTO() { Success = true };

                    case "PasswordMismatch":
                        return new ResponseDTO() { Message = "Senha incorreta." };

                    case "PasswordTooShort":
                        return new ResponseDTO() { Message = "A senha precisa ter pelo menos 6 caracteres." };

                    case "PasswordRequiresUniqueChars":
                        return new ResponseDTO() { Message = "A senha precisa ter pelo menos três caracteres diferentes." };

                    case "PasswordRequiresDigit":
                        return new ResponseDTO() { Message = "A senha precisa conter um número" };

                    default:
                        return new ResponseDTO() { Message = "Falha ao alterar a senha." };
                }
            }
            catch
            {
                return new ResponseDTO() { Message = "Falha ao alterar a senha." };
            }
        }

        public async Task Logout()
        {
            await _accountService.Logout();
        }
    }
}
