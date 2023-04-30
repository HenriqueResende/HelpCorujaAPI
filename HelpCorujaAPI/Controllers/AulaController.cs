using HelpCorujaAPI.Model;
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
        public string getAula(string? materia, int? semestre, DateTime? data)
        {
            var retorno = new List<Aula>();

            var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

            var dt = new DataTable();

            using (var adapter = new SqlDataAdapter("ListaAulas", connection))
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
    }
}
