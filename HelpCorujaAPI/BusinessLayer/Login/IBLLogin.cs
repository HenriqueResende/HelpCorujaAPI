using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public interface IBLLogin
    {
        public string Login(LoginDto login);

        public bool Cadastro(UsuarioDto usuario);
    }
}
