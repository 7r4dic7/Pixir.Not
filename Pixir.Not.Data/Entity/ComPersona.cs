using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pixir.Not.Data.Generic;
using Pixir.Not.Data.Generic.Extensible.KeyActivable;

namespace Pixir.Not.Data.Entity
{
    public partial class ComPersona : IBaseTable<int>, IKeyActivableRegisterState<ComCatEstadoRegistro>
    {
        public override string ToString()
        {
            return this.strAPaterno + " " + this.strAMaterno + " "
                + this.strNombre;
        }
        partial void OnCreated()
        {
            this.initializeStatus(EnumRegisterState.Activo);
        }
    }
}
