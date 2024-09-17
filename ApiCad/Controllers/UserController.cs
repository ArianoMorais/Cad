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
        public async Task<ActionResult<User>> Get(long id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return user != null ? Ok(user) : NotFound("Usu�rio n�o encontrado.");
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao buscar usu�rio por ID");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]UserDto user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return StatusCode(201, "Usu�rio criado com sucesso");
            }
            catch (CadException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao criar usu�rio");
            }
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserDto userDto)
        {
            try
            {
                if (id != userDto.Id)
                    return BadRequest("O ID do usu�rio n�o corresponde ao fornecido.");

                await _userService.UpdateUserAsync(userDto);
                return StatusCode(201, "Usu�rio editado com sucesso");
            }
            catch (CadException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao atualizar o usu�rio");
            }
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound("Usu�rio n�o encontrado.");
                }

                await _userService.DeleteAsync(id);
                return StatusCode(201, "Usu�rio deletado com sucesso");
            }
            catch (CadException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                return HandleError(ex, "Erro ao deletar usu�rio");
            }
        }

        [HttpGet("List")]
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