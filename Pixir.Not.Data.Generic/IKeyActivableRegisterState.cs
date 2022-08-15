using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Generic
{
    public interface IKeyActivableRegisterState<T>
    {
        int idComCatEstadoRegistro { get; set; }
        T ComCatEstadoRegistro { get; set; }
    }
}
