using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public interface IBLTutor
    {
        public bool setTutor(Tutor tutor);

        public Tutor getTutor(string ra);
    }
}
