using HelpCorujaAPI.BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/curso")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        public readonly IBLCurso _Curso;

        public CursoController(IBLCurso curso)
        {
            _Curso = curso;
        }

        #region getCurso
        [HttpGet]
        [Route("getCurso")]
        [Authorize]
        public IActionResult getCurso()
        {
            try
            {
                var retorno = _Curso.getCurso();

                return Ok(new { Status = 200, Json = JsonConvert.SerializeObject(retorno) });
            }
            catch (Exception)
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });

            }
        }
        #endregion
    }
}
