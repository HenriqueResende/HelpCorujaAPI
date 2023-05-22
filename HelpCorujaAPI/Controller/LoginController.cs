using HelpCorujaAPI.Model;
using Microsoft.AspNetCore.Mvc;
using HelpCorujaAPI.BusinessLayer;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IBLLogin _Login;


        public LoginController(IBLLogin login)
        {
            _Login = login;
        }

        #region Login
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login login)
        {
            try
            {
                var token = _Login.Login(login);

                return Ok(new { Status = 200, Token = token });
            }
            catch (FormatException Ex)
            {
                return BadRequest(new { Status = 400, Mensagem = Ex.Message });
            }
            catch
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });
            }
        }
        #endregion

        #region Cadastro
        [HttpPost]
        [Route("cadastro")]
        public IActionResult Cadastro(Usuario usuario)
        {
            try
            {
                if(_Login.Cadastro(usuario))
                    return Ok(new { Status = 200, Mensagem = "Usuário cadastrado com sucesso." });

                else
                    return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });
            }
            catch (FormatException Ex)
            {
                return BadRequest(new { Status = 400, Mensagem = Ex.Message });
            }
            catch
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });
            }
        }
        #endregion
    }
}
