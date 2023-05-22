using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public interface IBLLogin
    {
        public string Login(Login login);

        public bool Cadastro(Usuario usuario);
    }
}
