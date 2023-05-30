using HelpCorujaAPI.BusinessLayer;
using HelpCorujaAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/aula")]
    [ApiController]
    public class AulaController : ControllerBase
    {
        public readonly IBLAula _Aula;

        public AulaController(IBLAula aula)
        {
            _Aula = aula;
        }

        #region getAula
        /// <summary>
        /// getAula
        /// </summary>
        /// <param name="materia"></param>
        /// <param name="semestre"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAula")]
        [Authorize]
        public IActionResult getAula(string? materia, int? semestre, DateTime? data)
        {
            try
            {
                var retorno = _Aula.getAula(materia, semestre, data);

                return Ok(new { Status = 200, Json = JsonConvert.SerializeObject(retorno) });
            }
            catch (Exception)
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });

            }
        }
        #endregion

        #region getAulaTutor
        [HttpGet]
        [Route("getAulaTutor")]
        [Authorize]
        public IActionResult getAulaTutor(string ra)
        {
            try
            {
                var retorno = _Aula.getAulaTutor(ra);

                return Ok(new { Status = 200, Json = JsonConvert.SerializeObject(retorno) });
            }
            catch (Exception)
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });

            }
        }
        #endregion

        #region setAula
        [HttpPost]
        [Route("setAula")]
        [Authorize]
        public IActionResult setAula(AulaSetDto aula)
        {
            try
            {
                if(_Aula.setAula(aula))
                    return Ok(new { Status = 200, Mensagem = "Aula cadastrada com sucesso." });

                else
                    return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });
            }
            catch (FormatException Ex)
            {
                return BadRequest(new { Status = 400, Mensagem = Ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });
            }
        }
        #endregion

        #region deleteAula
        [HttpDelete]
        [Route("deleteAula")]
        [Authorize]
        public IActionResult deleteAula(int codigoAula)
        {
            try
            {
                if (_Aula.deleteAula(codigoAula))
                    return Ok(new { Status = 200, Mensagem = "Aula deletada com sucesso." });

                else
                    return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });
            }
            catch (Exception)
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });

            }
        }
        #endregion
    }
}
