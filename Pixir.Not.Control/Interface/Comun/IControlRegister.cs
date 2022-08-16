using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Pixir.Not.Control.Interface.Comun
{
    public interface IControlRegister<T>
    {
        T Show(Form _forma);
    }
}
