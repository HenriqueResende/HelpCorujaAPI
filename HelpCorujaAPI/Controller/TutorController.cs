using HelpCorujaAPI.BusinessLayer;
using HelpCorujaAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/tutor")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        public readonly IBLTutor _Tutor;

        public TutorController(IBLTutor tutor)
        {
            _Tutor = tutor;
        }

        #region setTutor
        [HttpPost]
        [Route("setTutor")]
        [Authorize]
        public IActionResult setTutor(Tutor tutor)
        {
            try
            {
                if(_Tutor.setTutor(tutor))
                    return Ok(new { Status = 200, Mensagem = "Tutor cadastrado com sucesso." });

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

        #region getTutor
        [HttpGet]
        [Route("getTutor")]
        [Authorize]
        public IActionResult getTutor(string ra)
        {
            try
            {
                var retorno = _Tutor.getTutor(ra);

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
