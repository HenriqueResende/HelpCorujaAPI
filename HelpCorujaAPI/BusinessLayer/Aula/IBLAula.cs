using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public interface IBLAula
    {
        public List<Aula> getAula(string? materia, int? semestre, DateTime? data);

        public List<Aula> getAulaTutor(string ra);

        public bool setAula(string? ra, string? materia, DateTime? dataInicio, DateTime? dataFim);

        public bool deleteAula(int codigoAula);
    }
}
