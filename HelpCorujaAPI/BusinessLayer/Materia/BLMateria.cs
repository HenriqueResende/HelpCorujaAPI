using HelpCorujaAPI.DataLayer;
using HelpCorujaAPI.Model;

namespace HelpCorujaAPI.BusinessLayer
{
    public class BLMateria : IBLMateria
    {
        private readonly ICRUD _CRUD;

        public BLMateria(ICRUD crud)
        {
            _CRUD = crud;
        }

        #region getMateria
        /// <summary>
        /// getMateria
        /// </summary>
        /// <returns></returns>
        public List<string> getMateria()
        {
            return _CRUD.ListQuery<MateriaDto>("SELECT DISTINCT Materia AS Nome FROM Aula WHERE CodigoUsuario IS NULL ORDER BY Materia ASC").Select(x => x.Nome).ToList();
        }
        #endregion
    }
}
