#region Using
using Pixir.Not.Data.Entity;
using Pixir.Not.Data.Extended.Enum;
using Pixir.Not.View.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Pixir.Not.View.Views.Common.Persona
{
    public partial class frmPersonaManager : Form, ICXFormManager<ComPersona>
    {
        #region Variables
        private EnumAccionForm accionForm;
        private EnumStateForm stateForm;
        private ComPersona baseEntity;
        private DataContext dataContext;
        private bool ignoreClosing = false;
        private bool stateEdit = false;
        private EnumAccionDialog accionDialog;
        private EnumOperationType tipoOperacion;
        //private SegUsuario user;
        /// <summary>
        /// variable local que indica que boton se pulso(Aceptar, Cancelar o X)
        /// </summary>
        private EnumAccionButton accionButton = EnumAccionButton.XClick;
        /// <summary>
        /// variable que inhabilita la carga de EnumStateForm.WithChanges, durante la cargad de controles
        /// En el Proceso de edicion. se utiliza para controles diferentes a data gridview
        /// </summary>
        private bool validateStateFormControl = false;

        /// <summary>
        /// variable que inhabilita la carga de EnumStateForm.WithChanges, durante la carga  de controles
        /// en los metodos rowadded y RowRemove, se utiliza solo cuando se asignaran datos a un datagridview
        /// </summary>
        private bool validateStateFormDataGrid = false;

        /// <summary>
        /// Variable que permite cuenta las ocaciones que se entra en la seccion saveEntity
        /// </summary>
        private int countInsideSaveEntity = (int)Not.Data.Extended.Enum.EnumNumericValue.Cero;

        /// <summary>
        /// Variable de CatalogExceptionManager.
        /// </summary>
        private CatalogExceptionManager catalogExceptionManager = new CatalogExceptionManager();

        /// <summary>
        /// Variable de tipo ConexionExceptionManager.
        /// </summary>
        private ConexionExceptionManager conexionExceptionManager = new ConexionExceptionManager();

        #endregion
        #region Constructor

        public frmPersonaManager()
        {
            InitializeComponent();

        }
        #endregion

        #region Miembros de ICXFormManager<ComPersona>

        public ComPersona show(Form _parent, DataContext _dc, EnumOperationType _tipoOperacion, SegUsuario _segUsuario, PermisosPantallaManager _permisosPantallaManager)
        {
            try
            {
                return this.show(_parent, null, _dc, _tipoOperacion, _segUsuario, _permisosPantallaManager); ;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return null;

        }

        public ComPersona show(Form _parent, ComPersona _baseEntity, System.Data.Linq.DataContext _dc, EnumOperationType _tipoOperacion, SegUsuario _segUsuario, PermisosPantallaManager _permisosPantallaManager)
        {
            try
            {
                this.initializeComponent(_baseEntity, _dc, _tipoOperacion, _segUsuario, _permisosPantallaManager);
                this.ShowDialog(_parent);

            }
            catch (Exception _e)
            {
                //Si un catalogo esta vacio.
                if (catalogExceptionManager.CatalogException) { return null; }
                //Si no se puede conectar con el servidor de base de datos.
                if (conexionExceptionManager.ConexionException) { return null; }
                MessageBox.Show(_e.Message);

            }
            return this.BaseEntity;


        }

        public void loadInitialInformation(EnumOperationType _tipoOperacion)
        {
            try
            {
                this.BaseEntity = this.BaseEntity ?? new ComPersona();
                this.tipoOperacion = _tipoOperacion;
                this.setComboBoxInitial();
                this.setStatus();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        public void setEntityForm(EnumOperationType _tipoOperacion)
        {

            this.setTabIndex();
            this.setTextField();
            this.setDateTime();
            this.setCheckBox();
            this.setComboBox();
            this.setGridView();
            this.validateStateFormControl = true;

        }

        public void getEntityForm()
        {

            this.countInsideSaveEntity++;
            //obtiene los datos a la primera vez que el usuario acepta
            if (this.countInsideSaveEntity == (int)Not.Data.Extended.Enum.EnumNumericValue.Uno)
            {
                this.getComboBox();
                this.getTextField();
                this.getCheckbox();
                this.getDatetime();

            }
            //obtiene los datos, hasta que el usuario acepta despues de n veces de utilizar, los botones: aceptar, cancelar
            //si, no y cancelar estos ultimos de cuadro de dialogo emergente
            if (this.countInsideSaveEntity > (int)Not.Data.Extended.Enum.EnumNumericValue.Uno)
            {
                this.BaseEntity = this.BaseEntity ?? new ComPersona();
                this.getComboBox();
                this.getTextField();
                this.getCheckbox();
                this.getDatetime();

            }

        }

        public bool validateEntity()
        {

            return true;
        }

        public EnumAccionForm AccionForm
        {
            get { return this.accionForm; }
            set { this.accionForm = value; }
        }

        public EnumStateForm StateForm
        {
            get { return this.stateForm; }
            set { this.stateForm = value; }
        }

        public ComPersona BaseEntity
        {
            get { return this.baseEntity; }
            set { this.baseEntity = value; }
        }

        public DataContext DataContext
        {
            get { return this.dataContext; }
            set { this.dataContext = value; }
        }

        public String Titulo
        {
            set { lblAccion.Text = value; }
        }

        public bool IgnoreClosing
        {
            get
            {
                return this.ignoreClosing;
            }
            set
            {
                this.ignoreClosing = value;
            }
        }

        public bool StateEdit
        {
            get { return stateEdit; }
            set { stateEdit = value; }
        }

        public EnumAccionDialog AccionDialog
        {
            get { return accionDialog; }
            set { accionDialog = value; }
        }

        public SegUsuario User
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public void setProfilePermissions(PermisosPantallaManager _permisosPantallaManager) { }

        #endregion

        #region Metodos setComboBoxInitial

        private void setComboBoxInitial()
        {
            if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Agregar)
            {
                this.setAgregarComboBoxInitial();
            }
            if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Editar)
            {
                this.setEditarComboBoxInitial();
            }

        }

        private void setAgregarComboBoxInitial()
        {
            try
            {
                if (this.DataContext.GetTable<ComCatEstadoCivil>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatEstadoCivil.DataSource = this.DataContext.GetTable<ComCatEstadoCivil>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_ESTADO_CIVIL_VACIO);
                }

                if (this.DataContext.GetTable<ComCatNacionalidad>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatNacionalidad.DataSource = this.DataContext.GetTable<ComCatNacionalidad>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_NACIONALIDAD_VACIO);
                }

                if (this.DataContext.GetTable<ComCatOcupacion>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatOcupacion.DataSource = this.DataContext.GetTable<ComCatOcupacion>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_OCUPACION_VACIO);
                }

                if (this.DataContext.GetTable<ComCatSexo>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatSexo.DataSource = this.DataContext.GetTable<ComCatSexo>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_SEXO_VACIO);
                }

                if (this.DataContext.GetTable<ComCatRegimenMatrimonial>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatRegimenMatrimonial.DataSource = this.DataContext.GetTable<ComCatRegimenMatrimonial>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_REGIMEN_MATRIMONIAL_VACIO);
                }

            }
            catch (Exception _e)
            {
                conexionExceptionManager.throwExceptionInCatch(_e, this);
                //necesita el mensaje de la excepcion.
                catalogExceptionManager.closeForm(this);
                MessageBox.Show(_e.Message);

            }

        }

        private void setEditarComboBoxInitial()
        {
            try
            {
                if (this.DataContext.GetTable<ComCatEstadoCivil>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatEstadoCivil.DataSource = this.DataContext.GetTable<ComCatEstadoCivil>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_ESTADO_CIVIL_VACIO);
                }

                if (this.DataContext.GetTable<ComCatNacionalidad>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatNacionalidad.DataSource = this.DataContext.GetTable<ComCatNacionalidad>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_NACIONALIDAD_VACIO);
                }

                if (this.DataContext.GetTable<ComCatOcupacion>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatOcupacion.DataSource = this.DataContext.GetTable<ComCatOcupacion>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_OCUPACION_VACIO);
                }

                if (this.DataContext.GetTable<ComCatSexo>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatSexo.DataSource = this.DataContext.GetTable<ComCatSexo>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_SEXO_VACIO);
                }

                if (this.DataContext.GetTable<ComCatRegimenMatrimonial>().Count() > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.cmbComCatRegimenMatrimonial.DataSource = this.DataContext.GetTable<ComCatRegimenMatrimonial>().OrderBy(c => c.strValor);
                }
                else
                {
                    catalogExceptionManager.throwExceptionInCatalog(Not.Control.Comun.Properties.Resources.MES_COM_CAT_REGIMEN_MATRIMONIAL_VACIO);
                }


            }
            catch (Exception _e)
            {
                conexionExceptionManager.throwExceptionInCatch(_e, this);
                //necesita el mensaje de la excepcion.
                catalogExceptionManager.closeForm(this);
                MessageBox.Show(_e.Message);
            }

        }



        #endregion

    }
}
