using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Generic.Extensible.KeyActivable
{
    public static class ExtensibleKeyActivable
    {
        public static void initializeStatus<T>(this IKeyActivable<T> _entity, EnumStatus _initialStatus)
        {
            _entity.idComCatStatus = (int)_initialStatus;
        }
        public static void activeRegister<Key>(this IKeyActivable<Key> _entity, DataContext _dc)
            where Key : class, IBaseCatalog
        {
            _entity.ComCatStatus = _dc.GetTable<Key>().Where(c => c.id.Equals((int)EnumStatus.Activo)).First();
        }
        public static void inactiveRegister<Key>(this IKeyActivable<Key> _entity, DataContext _dc)
            where Key : class, IBaseCatalog
        {
            _entity.ComCatStatus = (_dc.GetTable<Key>().Where(c => c.id.Equals((int)EnumStatus.Inactivo)).First());
        }
    }
}
