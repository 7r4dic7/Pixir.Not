using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pixir.Not.Data.Generic;
namespace Pixir.Not.Data.Entity
{
    public partial class ComCatEstadoCivil : IBaseCatalog
    {
        public override string ToString()
        {
            return this.strValor;
        }
    }
}
