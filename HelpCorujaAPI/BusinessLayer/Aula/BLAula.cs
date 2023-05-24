using HelpCorujaAPI.DataLayer;
using HelpCorujaAPI.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace HelpCorujaAPI.BusinessLayer
{
    public class BLAula : IBLAula
    {
        private readonly ICRUD _CRUD;

        public BLAula(ICRUD crud)
        {
            _CRUD = crud;
        }

        #region getAula
        /// <summary>
        /// getAula
        /// </summary>
        /// <param name="materia"></param>
        /// <param name="semestre"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<AulaDto> getAula(string? materia, int? semestre, DateTime? data)
        {
            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@Materia", SqlDbType.VarChar), value = materia },
                new Param { sqlParameter = new SqlParameter("@Semestre", SqlDbType.Int), value = semestre },
                new Param { sqlParameter = new SqlParameter("@Data", SqlDbType.DateTime), value = data }
            };

            return _CRUD.ListProc<AulaDto>("ListarAulas", param);
        }
        #endregion

        #region getAulaTutor
        /// <summary>
        /// getAulaTutor
        /// </summary>
        /// <param name="ra"></param>
        /// <returns></returns>
        public List<AulaDto> getAulaTutor(string ra)
        {
            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@RA", SqlDbType.VarChar), value = ra }
            };

            return _CRUD.ListProc<AulaDto>("ListarAulasTutor", param);
        }
        #endregion

        #region setAula
        /// <summary>
        /// setAula
        /// </summary>
        /// <param name="ra"></param>
        /// <param name="materia"></param>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <returns></returns>
        public bool setAula(string? ra, string? materia, DateTime? dataInicio, DateTime? dataFim)
        {
            if (ra.IsNullOrEmpty())
                throw new FormatException("Informe o RA.");

            else if (materia.IsNullOrEmpty())
                throw new FormatException("Informe a matéria.");

            else if (!dataInicio.HasValue)
                throw new FormatException("Informe a data de início.");

            else if (!dataFim.HasValue)
                throw new FormatException("Informe a data de fim.");

            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@RA", SqlDbType.VarChar), value = ra },
                new Param { sqlParameter = new SqlParameter("@Materia", SqlDbType.VarChar), value = materia },
                new Param { sqlParameter = new SqlParameter("@DataInicio", SqlDbType.DateTime), value = dataInicio },
                new Param { sqlParameter = new SqlParameter("@DataFim", SqlDbType.DateTime), value = dataFim }
            };

            return _CRUD.ExecProc("InserirAula", param);
        }
        #endregion

        #region deleteAula
        /// <summary>
        /// deleteAula
        /// </summary>
        /// <param name="codigoAula"></param>
        /// <returns></returns>
        public bool deleteAula(int codigoAula)
        {
            var param = new List<Param>
            {
                new Param { sqlParameter = new SqlParameter("@CodigoAula", SqlDbType.Int), value = codigoAula }
            };

            return _CRUD.ExecProc("DeletarAula", param);
        }
        #endregion
    }
}
