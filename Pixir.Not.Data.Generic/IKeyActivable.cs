using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Generic
{
    public interface IKeyActivable<T>
    {
        int idComCatStatus { get; set; }
        T ComCatStatus { get; set; }
    }
}
