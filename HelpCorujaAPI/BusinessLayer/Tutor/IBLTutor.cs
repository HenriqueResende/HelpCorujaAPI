using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public interface IBLTutor
    {
        public bool setTutor(TutorDto tutor);

        public TutorDto getTutor(string ra);
    }
}
