using HelpCorujaAPI.DataLayer;
using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public class BLCurso : IBLCurso
    {
        private readonly ICRUD _CRUD;

        public BLCurso(ICRUD crud)
        {
            _CRUD = crud;
        }

        #region getCurso
        /// <summary>
        /// getCurso
        /// </summary>
        /// <returns></returns>
        public List<Curso> getCurso()
        {
            return _CRUD.ListQuery<Curso>("SELECT * FROM Curso ORDER BY Nome ASC");
        }
        #endregion
    }
}
