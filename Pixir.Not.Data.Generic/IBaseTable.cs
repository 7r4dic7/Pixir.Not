using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Generic
{
    public interface IBaseTable<Key>
    {
        Key id { get; set; }
    }
}
