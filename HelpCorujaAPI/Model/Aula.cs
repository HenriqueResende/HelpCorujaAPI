namespace HelpCorujaAPI.Model
{
    public class Aula
    {
        public int? Codigo { get; set; }

        public Materia Materia { get; set; }

        public Tutor Tutor { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
    }
}
