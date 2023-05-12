using HelpCorujaAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/aula")]
    [ApiController]
    public class AulaController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public AulaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region getAula
        [HttpGet]
        [Route("getAula")]
        [Authorize]
        public IActionResult getAula(string? materia, int? semestre, DateTime? data)
        {
            try
            {
                var retorno = new List<Aula>();

                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("ListaRAulas", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@Materia", SqlDbType.VarChar).Value = materia;
                    adapter.SelectCommand.Parameters.Add("@Semestre", SqlDbType.Int).Value = semestre;
                    adapter.SelectCommand.Parameters.Add("@Data", SqlDbType.DateTime).Value = data;

                    adapter.Fill(dt);
                };

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retorno.Add(new Aula
                        {
                            Codigo = dt.Rows[i]["Codigo"] == DBNull.Value ? null : Convert.ToInt32(dt.Rows[i]["Codigo"]),
                            Materia = dt.Rows[i]["Materia"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Materia"]),
                            NomeTutor = dt.Rows[i]["Nome"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Nome"]),
                            Semestre = dt.Rows[i]["Semestre"] == DBNull.Value ? null : Convert.ToInt32(dt.Rows[i]["Semestre"]),
                            DataInicio = dt.Rows[i]["DataInicio"] == DBNull.Value ? null : Convert.ToDateTime(dt.Rows[i]["DataInicio"]),
                            DataFim = dt.Rows[i]["DataFim"] == DBNull.Value ? null : Convert.ToDateTime(dt.Rows[i]["DataFim"]),
                            Contato = dt.Rows[i]["Contato"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Contato"]),
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

        #region getAulaTutor
        [HttpGet]
        [Route("getAulaTutor")]
        [Authorize]
        public IActionResult getAulaTutor(string ra)
        {
            try
            {
                var retorno = new List<Aula>();

                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("ListarAulasTutor", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = ra;

                    adapter.Fill(dt);
                };

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retorno.Add(new Aula
                        {
                            Codigo = dt.Rows[i]["Codigo"] == DBNull.Value ? null : Convert.ToInt32(dt.Rows[i]["Codigo"]),
                            Materia = dt.Rows[i]["Materia"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Materia"]),
                            NomeTutor = dt.Rows[i]["Nome"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Nome"]),
                            Semestre = dt.Rows[i]["Semestre"] == DBNull.Value ? null : Convert.ToInt32(dt.Rows[i]["Semestre"]),
                            DataInicio = dt.Rows[i]["DataInicio"] == DBNull.Value ? null : Convert.ToDateTime(dt.Rows[i]["DataInicio"]),
                            DataFim = dt.Rows[i]["DataFim"] == DBNull.Value ? null : Convert.ToDateTime(dt.Rows[i]["DataFim"]),
                            Contato = dt.Rows[i]["Contato"] == DBNull.Value ? null : Convert.ToString(dt.Rows[i]["Contato"]),
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

        #region setAula
        [HttpPost]
        [Route("setAula")]
        [Authorize]
        public IActionResult setAula(string ra, string materia, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("InserirAula", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@ra", SqlDbType.VarChar).Value = ra;
                    adapter.SelectCommand.Parameters.Add("@Materia", SqlDbType.VarChar).Value = materia;
                    adapter.SelectCommand.Parameters.Add("@DataInicio", SqlDbType.DateTime).Value = dataInicio;
                    adapter.SelectCommand.Parameters.Add("@DataFim", SqlDbType.DateTime).Value = dataFim;
                    adapter.Fill(dt);
                };

                return Ok(new { Status = 200, Mensagem = "Aula cadastrada com sucesso." });
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
                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("DeletarAula", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@CodigoAula", SqlDbType.Int).Value = codigoAula;

                    adapter.Fill(dt);
                };

                return Ok(new { Status = 200, Mensagem = "Aula deletada com sucesso." });
            }
            catch (Exception)
            {
                return BadRequest(new { Status = 500, Mensagem = "Algo deu errado, tente novamente mais tarde!" });

            }
        }
        #endregion
    }
}
