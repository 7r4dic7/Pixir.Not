using Pixir.Not.Data.Generic;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.View.Extended.Disconnect.Catalog
{
    public static class CtrlCatalogFilter
    {
        /// <summary>
        /// Metodo que carga los filtros una sola ocacion al cargar el modulo. para catalogos sin condicion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_dc"></param>
        /// <returns>IQueryable</returns>
        public static IQueryable<CatalogExchange> getCatalog<T>(DataContext _dc)
            where T : class, IBaseCatalog
        {
            return from catalog in  _dc.GetTable<T>()
                   orderby catalog.strValor
                   select new CatalogExchange(catalog);
        }
    }
}
