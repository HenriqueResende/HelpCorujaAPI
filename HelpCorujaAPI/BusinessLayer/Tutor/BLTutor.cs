using HelpCorujaAPI.DataLayer;
using HelpCorujaAPI.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace HelpCorujaAPI.BusinessLayer
{
    public class BLTutor : IBLTutor
    {
        private readonly ICRUD _CRUD;

        public BLTutor(ICRUD crud)
        {
            _CRUD = crud;
        }

        #region setTutor
        /// <summary>
        /// setTutor
        /// </summary>
        /// <param name="tutor"></param>
        /// <returns></returns>
        public bool setTutor(TutorDto tutor)
        {
            if (tutor.RA.IsNullOrEmpty())
                throw new FormatException("Informe o RA.");

            else if (!tutor.CodigoCurso.HasValue)
                throw new FormatException("Informe o curso.");

            else if (!tutor.Semestre.HasValue)
                throw new FormatException("Informe o semestre.");

            else if (tutor.Contato.IsNullOrEmpty())
                throw new FormatException("Informe o contato.");

            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@RA", SqlDbType.VarChar), value = tutor.RA },
                new Param { sqlParameter = new SqlParameter("@CodigoCurso", SqlDbType.Int), value = tutor.CodigoCurso },
                new Param { sqlParameter = new SqlParameter("@Semestre", SqlDbType.Int), value = tutor.Semestre },
                new Param { sqlParameter = new SqlParameter("@Contato", SqlDbType.VarChar), value = tutor.Contato }
            };

            return _CRUD.ExecProc("InserirTutor", param);
        }
        #endregion

        #region getTutor
        /// <summary>
        /// getTutor
        /// </summary>
        /// <param name="ra"></param>
        /// <returns></returns>
        public TutorDto getTutor(string ra)
        {
            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@RA", SqlDbType.VarChar), value = ra }
            };

            return _CRUD.ListProc<TutorDto>("GetTutor", param).FirstOrDefault(new TutorDto());
        }
        #endregion
    }
}
