using Devagram.DTOs;
using Devagram.Models;
using Devagram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devagram.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // puxa o nome do controller se tiver nesse padrao. no caso de login,
                                // fica api/Login(login)
    public class LoginController : ControllerBase
    {
        // registro de logs
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO loginData)
        {
            try
            {
                bool isLoginDataValid = !string.IsNullOrEmpty(loginData.Password)
                    && !string.IsNullOrEmpty(loginData.Email)
                    && !string.IsNullOrWhiteSpace(loginData.Password)
                    && !string.IsNullOrWhiteSpace(loginData.Email);

                if (isLoginDataValid)
                {
                    string email = "lucas.carmo@meta.com.br";
                    string password = "Senha@123";

                    if (loginData.Email == email && loginData.Password == password)
                    {
                        User user = new()
                        {
                            Id = 12,
                            Name = "Lucas Pasche",
                            Email = loginData.Email,
                        };
                        return Ok(new LoginResponseDTO()
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Token = TokenService.SetToken(user)
                        });
                    }
                    else
                    {
                        return BadRequest(new ErrorResponseDTO()
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Message = "Dados de usuário incorretos."
                        });
                    }
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Usuário não preencheu os campos de login corretamente."
                    });
                }

                if (loginData.Email == null)
                {
                    throw new ArgumentNullException("Por favor, insira o email.");
                }
            }
            catch(Exception error)
            {
                _logger.LogError($"Ocorreu um erro no login: {error.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Ocorreu um erro ao fazer o login."
                });
            }
        }
    }
}
