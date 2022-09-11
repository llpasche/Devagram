using Devagram.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devagram.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // puxa o nome do controller se tiver nesse padrao. no caso de login,
                                // fica api/Login
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
                throw new ArgumentException("Erro ao preencher os dados");
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
