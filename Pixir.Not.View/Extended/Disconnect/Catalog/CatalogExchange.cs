using Pixir.Not.Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixir.Not.View.Extended.Disconnect.Catalog
{
    public class CatalogExchange : IBaseCatalog
    {
        #region Variables
        private int _id;
        private string _strValor;
        private string _strDescripcion;
        #endregion

        #region Constructor
        public CatalogExchange()
        {

        }
        /// <summary>
        /// Constructor que llena los datos que viene de la bd para los filtros
        /// </summary>
        /// <param name="_baseCatalog"></param>
        public CatalogExchange(IBaseCatalog _baseCatalog)
        {
            this._id = _baseCatalog.id;
            this._strValor = (_baseCatalog.strValor != null) ? _baseCatalog.strValor.Clone().ToString() : null;
            this._strDescripcion = (_baseCatalog.strDescripcion != null) ? _baseCatalog.strDescripcion.Clone().ToString() : null;
        }
        #endregion

        #region Miembros de IBaseCatalog
        public string strDescripcion
        {
            get { return _strDescripcion; }
            set { _strDescripcion = value; }
        }
        public string strValor
        { get { return _strValor; } set { _strValor = value; } }

        #endregion

        #region Miembros de IBaseTable<int>
        public int id { get { return _id; } set { _id = value; } }
        #endregion
    }
}
