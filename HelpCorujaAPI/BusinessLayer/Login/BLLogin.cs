using HelpCorujaAPI.DataLayer;
using HelpCorujaAPI.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace HelpCorujaAPI.BusinessLayer
{
    public class BLLogin : IBLLogin
    {
        private readonly ICRUD _CRUD;
        private readonly IConfiguration _configuration;
        private readonly IBLCriptografia _criptografia;

        public BLLogin(ICRUD crud, IConfiguration configuration, IBLCriptografia criptografia)
        {
            _CRUD = crud;
            _configuration = configuration;
            _criptografia = criptografia;
        }

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public string Login(Login login)
        {
            if (login.RA.IsNullOrEmpty())
                throw new FormatException("Informe o RA.");

            else if (login.Senha.IsNullOrEmpty())
                throw new FormatException("Informe a Senha.");

            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@RA", SqlDbType.VarChar), value = login.RA }
            };

            var usuario = _CRUD.ListProc<Usuario>("ObterUsuario", param).FirstOrDefault();

            if (usuario == null || usuario?.Codigo == null || usuario.Codigo == 0)
                throw new FormatException("Usuário não cadastrado.");


            if (_criptografia.Check(usuario.Senha, login.Senha))
                return _criptografia.CreateToken(login.RA);

            else
                throw new FormatException("Login ou senha inválidos.");
        }
        #endregion

        #region Cadastro
        /// <summary>
        /// Cadastro
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Cadastro(Usuario usuario)
        {
            if (usuario.RA.IsNullOrEmpty())
                throw new FormatException("Informe o RA.");

            else if (usuario.Nome.IsNullOrEmpty())
                throw new FormatException("Informe o Nome.");

            else if (usuario.Senha.IsNullOrEmpty())
                throw new FormatException("Informe a Senha.");

            usuario.Senha = _criptografia.Hash(usuario.Senha);

            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@Nome", SqlDbType.VarChar), value = usuario.Nome },
                new Param { sqlParameter = new SqlParameter("@RA", SqlDbType.VarChar), value = usuario.RA },
                new Param { sqlParameter = new SqlParameter("@Senha", SqlDbType.VarChar), value = usuario.Senha }
            };

            _CRUD.ExecProc("InserirUsuario", param);

            return true;
        }
        #endregion
    }
}
