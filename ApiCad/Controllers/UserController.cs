using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Application;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Domain.Exceptions;
using UserModule.Domain.Entities;
using UserModule.Domain.Services;

namespace ApiCad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
            : base(logger)
        {
            _userService = userService;
        }

        [HttpGet("Get/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get(string id)
        {
            try
            {
                var user = await _userService.GetUserDtoByIdAsync(id);
                return user != null ? Ok(user) : NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao buscar usuário por ID");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var data = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);

                if (data == null)
                    return Unauthorized("Credenciais inválidas.");

                return Ok(new { data });
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao fazer login");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]UserDto user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return StatusCode(201, "Usuário criado com sucesso");
            }
            catch (CadException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao criar usuário");
            }
        }

        [HttpPost("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto.Id != userDto.Id)
                    return BadRequest("O ID do usuário não corresponde ao fornecido.");

                await _userService.UpdateUserAsync(userDto);
                return StatusCode(201, "Usuário editado com sucesso");
            }
            catch (CadException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao atualizar o usuário");
            }
        }

        [HttpPost("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);

                if (user == null)
                    return NotFound("Usuário não encontrado.");

                await _userService.DeleteAsync(id);
                return StatusCode(201, "Usuário deletado com sucesso");
            }
            catch (CadException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao deletar usuário");
            }
        }

        [HttpGet("List")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> List()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Error fetching users");
            }
        }
    }
}