using HelpCorujaAPI.BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/materia")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        public readonly IBLMateria _Materia;

        public MateriaController(IBLMateria materia)
        {
            _Materia = materia;
        }

        #region getMateria
        [HttpGet]
        [Route("getMateria")]
        [Authorize]
        public IActionResult getMateria()
        {
            try
            {
                var retorno = _Materia.getMateria();

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
