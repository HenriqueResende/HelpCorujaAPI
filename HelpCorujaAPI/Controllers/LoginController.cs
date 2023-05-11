using HelpCorujaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HelpCorujaAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit
        private int Interations;


        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;

            Interations = _configuration.GetValue<int>("Iterations");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login login)
        {
            try
            {
                var usuario = new Usuario();

                if (login.RA.IsNullOrEmpty())
                    return BadRequest("Informe o RA.");

                else if (login.Senha.IsNullOrEmpty())
                    return BadRequest("Informe a Senha.");

                #region busca o usuário no banco
                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("ObterUsuario", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = login.RA;

                    adapter.Fill(dt);
                };

                if (dt.Rows.Count > 0)
                {
                    usuario = new Usuario
                    {
                        Codigo = dt.Rows[0]["Codigo"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Codigo"]),
                        Nome = dt.Rows[0]["Nome"] == DBNull.Value ? null : Convert.ToString(dt.Rows[0]["Nome"]),
                        RA = dt.Rows[0]["RA"] == DBNull.Value ? null : Convert.ToString(dt.Rows[0]["RA"]),
                        Senha = dt.Rows[0]["Senha"] == DBNull.Value ? null : Convert.ToString(dt.Rows[0]["Senha"])
                    };
                }
                #endregion

                if(usuario?.Codigo == null || usuario.Codigo == 0)
                    return BadRequest("Usuário não cadastrado.");

                if (Check(usuario.Senha, login.Senha))
                {
                    var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, login.RA)
                        }),
                        Expires = DateTime.UtcNow.AddDays(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new { Token = tokenHandler.WriteToken(token) });
                }
                else
                    return Unauthorized();
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao gerar o token.");
            }
        }

        [HttpPost]
        [Route("cadastro")]
        public IActionResult Cadastro(Usuario usuario)
        {
            try
            {
                if (usuario.RA.IsNullOrEmpty())
                    return BadRequest("Informe o RA.");

                else if (usuario.Nome.IsNullOrEmpty())
                    return BadRequest("Informe o Nome.");

                else if (usuario.Senha.IsNullOrEmpty())
                    return BadRequest("Informe a Senha.");

                usuario.Senha = Hash(usuario.Senha);

                #region Salva o usuário
                var connection = new SqlConnection(_configuration.GetConnectionString("HelpCorujaAppCon").ToString());

                var dt = new DataTable();

                using (var adapter = new SqlDataAdapter("InserirUsuario", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add("@Nome", SqlDbType.VarChar).Value = usuario.Nome;
                    adapter.SelectCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = usuario.RA;
                    adapter.SelectCommand.Parameters.Add("@Senha", SqlDbType.VarChar).Value = usuario.Senha;

                    adapter.Fill(dt);
                };
                #endregion

                return Ok(true);
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao gerar o token.");
            }
        }

        #region Métodos de criptografia
        private string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Interations,
              HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Interations}.{salt}.{key}";
            }
        }

        private bool Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);


            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return verified;
            }
        }
        #endregion
    }
}
