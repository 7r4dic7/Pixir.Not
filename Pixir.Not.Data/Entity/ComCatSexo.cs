﻿using Pixir.Not.Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Data.Entity
{
    public partial class ComCatSexo : IBaseCatalog
    {
        public override string ToString()
        {
            return this.strValor;
        }
    }
}
