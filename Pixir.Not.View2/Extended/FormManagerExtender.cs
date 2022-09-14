using Pixir.Not.Data.Extended.Enum;
using Pixir.Not.View.Common;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixir.Not.View.Extended
{
    /// <summary>
    /// Clase que contiene metodos extendidos XXXXX
    /// </summary>

    public static class FormManagerExtender
    {
        #region Metodos Extendidos

        /// <summary>
        /// Metodo extendido que sirve para cargar la pantalla manager, dependiendo si es nueva o edicion
        /// </summary>
        /// <typeparam name="T">Generico T</typeparam>
        /// <param name="_form">Formulario actual</param>
        /// <param name="_baseEntity">Entidad actual</param>
        /// <param name="_dataContext">Datacontext</param>  
        public static void initializeComponent<T>(this ICXFormManager<T> _form, T _baseEntity, DataContext _dc, EnumOperationType _tipoOperacion)
        {
            _form.BaseEntity = _baseEntity;
            _form.DataContext = _dc;
            

            //si la accion es editar entra en este if y asigna accion y titulo de pantalla
            if (_form.BaseEntity != null)
            {
                _form.AccionForm = EnumAccionForm.Edit;
                //_form.Titulo = Not.Control.Comun.Properties.Resources.TIT_MODULO_EDITAR;

            }
            //si la accion es nueva entra en este if y asigna accion y titulo de pantalla
            else
            {
                _form.AccionForm = EnumAccionForm.New;
               // _form.Titulo = Not.Control.Comun.Properties.Resources.TIT_MODULO_NUEVO;

            }

            _form.loadInitialInformation(_tipoOperacion); //Carga la informacion personalizada de la pantalla
            _form.setEntityForm(_tipoOperacion); //Se carga en pantalla y se sincronizan controles
            //_form.setProfilePermissions(_permisosPantallaManager);//estab;ece los permisos por pantalla manager

            _form.StateForm = EnumStateForm.WithOutChanges;
        }

        /// <summary>
        /// Metodo extensible que obtiene de la pantalla manager los datos
        /// y valida los datos.
        /// </summary>
        /// <typeparam name="T">Generico</typeparam>
        /// <param name="_form">form actual</param>
        /// <returns></returns>
        public static bool saveEntity<T>(this ICXFormManager<T> _form)
        {
            _form.getEntityForm();
            return _form.validateEntity();
        }

        /// <summary>
        /// metodo que verifica si existe algun cambio en la pantalla que se pueda almacenar
        /// </summary>
        /// <typeparam name="T">ICXFormManager</typeparam>
        /// <param name="_form">_form</param>
        /// <param name="_e">FormClosingEventArgs</param>
        public static void formClosing<T>(this ICXFormManager<T> _form, FormClosingEventArgs _e)
        {
            if (_form.IgnoreClosing)
            {
                return;
            }

            if (_form.StateForm == EnumStateForm.WithChanges)
            {
                if (_form.AccionDialog == EnumAccionDialog.Si)
                {
                    _form.saveEntity();
                    return;
                }
                if (_form.AccionDialog == EnumAccionDialog.No)
                {
                    if (_form.AccionForm == EnumAccionForm.New)
                    {
                        _form.BaseEntity = default(T);
                    }
                    if (_form.AccionForm == EnumAccionForm.Edit)
                    {
                        _form.BaseEntity = default(T);
                    }
                    return;

                }
                if (_form.AccionDialog == EnumAccionDialog.Cancel)
                {
                    _e.Cancel = true;
                    return;
                }
                if (_form.AccionDialog == EnumAccionDialog.Null)
                {
                    _e.Cancel = true;
                    return;
                }

            }

            if (_form.StateForm == EnumStateForm.WithOutChanges)
            {
                if (_form.AccionDialog == EnumAccionDialog.Null)
                {
                    _e.Cancel = true;
                    return;
                }

                if (_form.AccionForm == EnumAccionForm.New)
                {
                    _form.BaseEntity = default(T);
                    return;
                }
                if (_form.AccionForm == EnumAccionForm.Edit)
                {
                    _form.BaseEntity = default(T);
                    return;
                }
            }

            if (!_form.IgnoreClosing)
            {
                _form.BaseEntity = default(T);
            }
        }
        #endregion

    }
}
