using System.Data.SqlClient;

namespace HelpCorujaAPI.Model
{
    public class Param
    {
        public SqlParameter sqlParameter { get; set; }

        public object value { get; set; }
    }
}
