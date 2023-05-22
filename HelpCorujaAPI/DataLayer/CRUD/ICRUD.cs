using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.DataLayer
{
    public interface ICRUD
    {
        public List<Model> ListQuery<Model>(string query);

        public bool ExecProc(string proc, List<Param>? param = null);

        public List<Model> ListProc<Model>(string proc, List<Param>? param = null);
    }
}
