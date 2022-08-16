using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.Control.Interface.Comun
{
    public interface IRegisterRules
    {
        /// <summary>
        /// Mensaje almacenado en la regla de nogocios despues de la ultima operacion ejecutada
        /// </summary>
        string Mensaje { get; }
        /// <summary>
        /// Valida la regla de negocio de acuerdo con el elemento recibido
        /// </summary>
        /// <param name="_elementoValidar"></param>
        /// <param name="_dataContext"></param>
        /// <returns></returns>
        bool validaRegistro(object _elementoValidar, DataContext _dataContext);
    }
}
