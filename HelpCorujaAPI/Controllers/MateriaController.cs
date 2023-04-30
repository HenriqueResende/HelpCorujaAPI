using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/materia")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public MateriaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getMateria")]
        public string getMateria()
        {
            var retorno = new List<string>();

            var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

            var dt = new DataTable();

            var adapter = new SqlDataAdapter("SELECT DISTINCT Materia FROM Aula", connection);

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    retorno.Add(Convert.ToString(dt.Rows[i]["Materia"]));
                }
            }

            return JsonConvert.SerializeObject(retorno);
        }
    }
}
