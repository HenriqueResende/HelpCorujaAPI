using Microsoft.IdentityModel.Tokens;

namespace HelpCorujaAPI.Model
{
    public class Usuario
    {
        public Usuario()
        {
            Nome = new Nome();
            Senha = new Senha();
            RA = new RA();
        }

        public Usuario(string nome, string ra, string senha)
        {
            Nome = new Nome(nome);
            Senha = new Senha(senha);
            RA = new RA(ra);
        }

        public int Codigo { get; set; }

        public Nome Nome;

        public RA RA;

        public Senha Senha;
    }

    #region RA
    public class RA
    {
        private string valor { get; set; }

        public RA(){ }

        public RA(string ra)
        {
            setRA(ra);
        }

        public void setRA(string ra)
        {
            if (ra.IsNullOrEmpty())
                throw new FormatException("Informe o RA.");

            if (ra.Length != 6)
                throw new FormatException("O RA deve conter 6 caracteres.");

            valor = ra;
        }

        public string getRA()
        {
            return valor;
        }
    }
    #endregion

    #region Nome
    public class Nome
    {
        private string valor { get; set; }

        public Nome() { }

        public Nome(string nome)
        {
            setNome(nome);
        }

        public void setNome(string nome)
        {
            if (nome.IsNullOrEmpty())
                throw new FormatException("Informe o nome.");

            if (nome.Split(" ").Count() < 2)
                throw new FormatException("Informe o nome completo.");

            valor = nome;
        }

        public string getNome()
        {
            return valor;
        }
    }
    #endregion

    #region Senha
    public class Senha
    {
        private string valor { get; set; }

        public Senha() { }

        public Senha(string senha)
        {
            setSenha(senha);
        }

        public void setSenha(string senha)
        {
            if (senha.IsNullOrEmpty())
                throw new FormatException("Informe a senha.");

            if (senha.Length < 6)
                throw new FormatException("A senha deve conter no mínimo 6 caracteres.");

            valor = senha;
        }

        public string getSenha()
        {
            return valor;
        }
    }
    #endregion
}
