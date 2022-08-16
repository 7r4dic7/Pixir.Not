using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Generic.Extensible.KeyActivable
{
    public static class ExtensibleKeyActivableRegisterState
    {
        public static void initializeStatus<T>(this IKeyActivableRegisterState<T> _entity, EnumRegisterState _initialRegisterState)
        {
            _entity.idComCatEstadoRegistro = (int)_initialRegisterState;
        }
        public static void activeRegister<Key>(this IKeyActivableRegisterState<Key> _entity, DataContext _dc)
            where Key : class, IBaseCatalog
        {
            _entity.ComCatEstadoRegistro = _dc.GetTable<Key>().Where(c => c.id.Equals((int)EnumRegisterState.Activo)).First();
        }
        public static void inactiveRegister<Key>(this IKeyActivableRegisterState<Key> _entity, DataContext _dc)
            where Key: class, IBaseCatalog
        {
            _entity.ComCatEstadoRegistro = _dc.GetTable<Key>().Where(c => c.id.Equals((int)EnumRegisterState.Eliminado)).First();
        }
    }
}
