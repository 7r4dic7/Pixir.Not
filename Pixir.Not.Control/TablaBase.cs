using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq.Mapping;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Linq;

using Pixir.Not.Data.Entity;
using System.Windows.Forms;

namespace Pixir.Not.Control
{
    public class TablaBase<T> where T : class
    {
        protected TablaBase()
        {

        }
        /// <summary>
        /// Metodo que regresa el contenido de la tabla
        /// </summary>
        /// <param name="_dc">Datacontex</param>
        /// <param name="_predicate">Expression</param>
        /// <returns></returns>
        protected IQueryable<T> select(DataContext _dc, Expression<Func<T, bool>> _predicate)
        {
            try
            {
                return _dc.GetTable<T>().Where(_predicate);
            }

            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return null;

        }
        /// <summary>
        /// Metodo que obtiene los datos de una tabla
        /// </summary>
        /// <param name="_dc">DataContext</param>
        /// <returns>IQueryable</returns>
        protected IQueryable<T> select(DataContext _dc)
        {
            try
            {
                return (IQueryable<T>)_dc.GetTable<T>();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            return null;


        }

        protected void insert(DataContext _dc, T _entity)
        {
            try
            {
                _dc.GetTable<T>().InsertOnSubmit(_entity);
            }

            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }


        }
    }
}
