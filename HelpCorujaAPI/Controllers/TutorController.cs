using HelpCorujaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/tutor")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public TutorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region setTutor
        [HttpPost]
        [Route("setTutor")]
        [Authorize]
        public IActionResult setTutor(Tutor tutor)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("InserirTutor", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = tutor.RA;
                    adapter.SelectCommand.Parameters.Add("@CodigoCurso", SqlDbType.Int).Value = tutor.CodigoCurso;
                    adapter.SelectCommand.Parameters.Add("@Semestre", SqlDbType.Int).Value = tutor.Semestre;
                    adapter.SelectCommand.Parameters.Add("@Contato", SqlDbType.VarChar).Value = tutor.Contato;

                    adapter.Fill(dt);
                };

                return Ok(new { Status = 200, Mensagem = "Tutor cadastrado com sucesso." });
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
                var retorno = new Tutor();

                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("GetTutor", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = ra;

                    adapter.Fill(dt);
                };

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retorno = new Tutor
                        {
                            RA = dt.Rows[i]["Codigo"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["RA"]),
                            CodigoCurso = Convert.ToInt32(dt.Rows[i]["CodigoCurso"]),
                            Semestre = Convert.ToInt32(dt.Rows[i]["Semestre"]),
                            Contato = dt.Rows[i]["Contato"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Contato"]),
                        };
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
