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

        [HttpGet]
        [Route("getAula")]
        [Authorize]
        public string getAula(string? materia, int? semestre, DateTime? data)
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

            return JsonConvert.SerializeObject(retorno);
        }

        [HttpGet]
        [Route("getAulaTutor")]
        [Authorize]
        public string getAulaTutor(string ra)
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

            return JsonConvert.SerializeObject(retorno);
        }

        [HttpPost]
        [Route("setAula")]
        [Authorize]
        public string setAula(string ra, string materia, DateTime dataInicio, DateTime dataFim)
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

            return JsonConvert.SerializeObject(true);
        }

        [HttpDelete]
        [Route("deleteAula")]
        [Authorize]
        public string deleteAula(int codigoAula)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter("DeletarAula", connection))
            {
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add("@CodigoAula", SqlDbType.Int).Value = codigoAula;

                adapter.Fill(dt);
            };

            return JsonConvert.SerializeObject(true);
        }
    }
}
