using Pixir.Not.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pixir.Not.Control.Control.Comun
{
    public class CtrlPersona: TablaBase<ComPersona>
    {
        /// <summary>
        /// Retorna la lista de personas que coincidan con la expresion recibida
        /// </summary>
        /// <param name="_dataContext">Data context para ejecutar la operacion</param>
        /// <param name="_predicate">Expresión con la cual deben coincidir los elementos devueltos</param>
        /// <returns>Lista de elementos que coinciden con la expresion</returns>
        public List<ComPersona> getListItemByExpression(DataContext _dataContext, Expression<Func<ComPersona, bool>> _predicate)
        {
            try
            {
                return this.select(_dataContext, _predicate).ToList<ComPersona>();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return null;
        }


        /// <summary>
        /// Metodo que obtiene una persona que cumpla con la condicion de Id
        /// </summary>
        /// <param name="_dataContext">DataContext</param>
        /// <param name="_comPersona">ComPersona</param>
        /// <returns>ComPersona</returns>
        public ComPersona getPersonaEmpleado(DataContext _dataContext, Expression<Func<Not.Data.Entity.ComPersona,bool>> _predicate)
        {
            try
            {

                base.select(_dataContext, _predicate).ToList<ComPersona>();

                foreach (ComPersona _temp in base.select(_dataContext, _predicate).ToList<ComPersona>())
                {
                    return _temp;
                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return null;

        }

    }
}
