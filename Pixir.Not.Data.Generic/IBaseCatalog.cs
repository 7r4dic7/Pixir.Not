using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Generic
{
    internal interface IBaseCatalog : IBaseTable<int>
    {
        String strValor { get; set; }
        String strDescripcion { get; set; }
    }
}
