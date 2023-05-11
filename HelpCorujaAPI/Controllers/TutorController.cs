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

        [HttpPost]
        [Route("setTutor")]
        [Authorize]
        public string setTutor(Tutor tutor)
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

            return JsonConvert.SerializeObject(true);
        }
    }
}
