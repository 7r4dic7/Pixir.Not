using Pixir.Not.Data.Generic;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Control
{
    public class CtrlGeneric
    {
        /// <summary>
        /// Metodo que realiza una consulta por expresion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dataContext"></param>
        /// <param name="_predicate"></param>
        /// <returns></returns>
        public static List<T> getListByExpression<T>(DataContext _dataContext, Expression<Func<T,bool>> _predicate)
            where T : class, IBaseTable<int>
        {
            return _dataContext.GetTable<T>().Where(_predicate).ToList();
        }
        /// <summary>
        /// Metodo que obtiene un elemento tipo T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dataContext"></param>
        /// <param name="_predicate"></param>
        /// <returns></returns>
        public static T getItemByExpression<T>(DataContext _dataContext, Expression<Func<T,bool>> 
            _predicate) where T : class, IBaseTable<int>
        {
            return _dataContext.GetTable<T>().FirstOrDefault(_predicate);
        }
        public static List<T> getListByExpression<T>(DataContext _dataContext) where T : class, IBaseTable<int>
        {
            return _dataContext.GetTable<T>().ToList();
        }
        public static IQueryable<T> getIQueryableEntity<T>(DataContext _dataContext) where T : class, IBaseTable<int>
        {
            return _dataContext.GetTable<T>();
        }
        public static IQueryable<T> getIQueryableEntityByExpression<T>(DataContext _dataContext, Expression<Func<T, bool>> _predicate) where T : class, IBaseTable<int>
        {
            return _dataContext.GetTable<T>().Where(_predicate);
        }
        public static object getListByExpression<T1>()
        {
            throw new NotImplementedException();
        }
    }
}
