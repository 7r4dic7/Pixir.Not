using Pixir.Not.Data.Generic;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.View.Common
{
    public class Compiled<T> where T : class, IBaseTable<int>
    {
        // List<ComPersona> lista = Not.View.Common.Compiled<ComPersona>.generateQueryCompile(this.dataContext, predicate);
        public static List<T> generateQueryCompile (DataContext _dataContext, Expression<Func<T, bool>> _expression)
        {
            return _dataContext.GetTable<T>().Where(_expression.Compile()).ToList();
        }
        public static Func<DataContext, IQueryable<T>> queryCompiled = CompiledQuery.Compile((DataContext dc)
            => dc.GetTable<T>().Where(c => c.id > (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero));
        public IQueryable<T> getListEntity(DataContext _dataContext)
        {
            return queryCompiled(_dataContext);
        }
    }
}
