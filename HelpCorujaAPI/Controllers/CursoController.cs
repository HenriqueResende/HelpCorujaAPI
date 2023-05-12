using HelpCorujaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/curso")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public CursoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region getCurso
        [HttpGet]
        [Route("getCurso")]
        [Authorize]
        public IActionResult getCurso()
        {
            try
            {
                var retorno = new List<Curso>();

                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                var adapter = new SqlDataAdapter("SELECT * FROM Curso ORDER BY Nome ASC", connection);

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retorno.Add(new Curso
                        {
                            Codigo = Convert.ToInt32(dt.Rows[i]["Codigo"]),
                            Nome = Convert.ToString(dt.Rows[i]["Nome"])
                        });
                    }
                }

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
