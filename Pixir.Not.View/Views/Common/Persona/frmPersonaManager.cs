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

        public ComPersona show(Form _parent, DataContext _dc, EnumOperationType _tipoOperacion)
        {
            try
            {
                return this.show(_parent, null, _dc, _tipoOperacion); ;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return null;

        }

        public ComPersona show(Form _parent, ComPersona _baseEntity, System.Data.Linq.DataContext _dc, EnumOperationType _tipoOperacion)
        {
            try
            {
                this.initializeComponent(_baseEntity, _dc, _tipoOperacion);
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


        public void setProfilePermissions() { }

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

        #region Metodos Set 

        private void setStatus()
        {
            if (this.Size.Height < 688)
            {
                this.Size = new System.Drawing.Size(this.Size.Width, (int)Not.Data.Extended.Enum.EnumResizeForm.SizeWidthManager);
            }
            if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Agregar)
            {
                this.chkDocumentoLegalEstancia.Enabled = false;
                this.cmbComCatRegimenMatrimonial.Enabled = true;
            }
            if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Editar)
            {
            }

        }

        private void setTabIndex()
        {
            this.txtNombre.TabIndex = 0;
            this.txtApellidoPaterno.TabIndex = 1;
            this.txtApellidoMaterno.TabIndex = 2;
            this.dteFechaNacimiento.TabIndex = 3;
            this.cmbComCatSexo.TabIndex = 4;
            this.cmbComCatEstadoCivil.TabIndex = 5;
            this.cmbComCatRegimenMatrimonial.TabIndex = 6;
            this.cmbComCatOcupacion.TabIndex = 7;
            this.txtCurp.TabIndex = 8;
            this.txtFolioIfe.TabIndex = 9;
            this.txtOriginario.TabIndex = 10;
            this.cmbComCatNacionalidad.TabIndex = 11;
            this.chkDocumentoLegalEstancia.TabIndex = 12;
            this.btnAgregarDatoContacto.TabIndex = 13;
            this.btnEditarDatoContacto.TabIndex = 14;
            this.btnEliminarDatoContacto.TabIndex = 15;
            this.dgvComDatoContacto.TabIndex = 16;
            this.btnAceptar.TabIndex = 17;
            this.btnCancelar.TabIndex = 18;
        }

        private void setTextField()
        {
            try
            {
                this.txtNombre.Text = this.BaseEntity.strNombre ?? String.Empty;
                this.txtApellidoPaterno.Text = this.BaseEntity.strAPaterno ?? String.Empty;
                this.txtApellidoMaterno.Text = this.BaseEntity.strAMaterno ?? String.Empty;
                this.txtCurp.Text = this.BaseEntity.strCURP ?? String.Empty;
                this.txtFolioIfe.Text = this.BaseEntity.strFolioIFE ?? String.Empty;
                this.txtOriginario.Text = this.BaseEntity.strOriginario ?? String.Empty;

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setDateTime()
        {
            try
            {
                this.dteFechaNacimiento.Value = this.BaseEntity.dteFechaNacimiento ?? DateTime.Now;
                //this.dteFechaNacimiento.Value = this.dteFechaNacimiento.Value.Date;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void setCheckBox()
        {
            try
            {
                this.chkDocumentoLegalEstancia.Checked = this.BaseEntity.bitDocEstancia ?? this.chkDocumentoLegalEstancia.Checked;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void setComboBox()
        {
            try
            {
                this.cmbComCatEstadoCivil.SelectedItem = this.BaseEntity.ComCatEstadoCivil;
                this.cmbComCatNacionalidad.SelectedItem = this.BaseEntity.ComCatNacionalidad;
                this.cmbComCatOcupacion.SelectedItem = this.BaseEntity.ComCatOcupacion;
                this.cmbComCatRegimenMatrimonial.SelectedItem = this.BaseEntity.ComCatRegimenMatrimonial;
                this.cmbComCatSexo.SelectedItem = this.BaseEntity.ComCatSexo;


            }
            catch (Exception _e)
            {
                conexionExceptionManager.throwExceptionInCatch(_e, this);
                MessageBox.Show(_e.Message);
            }
        }

        private void setGridView()
        {
            try
            {
                List<ComDatoContacto> comDatoContacto = new List<ComDatoContacto>();
                for (int i = 0; i < this.BaseEntity.ComDatoContacto.Count; i++)
                {
                    comDatoContacto.Add(this.BaseEntity.ComDatoContacto[i]);
                }
                for (int i = 0; i < this.BaseEntity.CliPersonaCliente.Count; i++)
                {
                    for (int j = 0; j < this.BaseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto.Count; j++)
                    {
                        if (this.BaseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto[j].ComPersona == null)
                        {
                            comDatoContacto.Add(this.BaseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto[j]);
                        }
                    }
                }
                dgvComDatoContacto.DataSource = new BindingSource(comDatoContacto, null);

            }
            catch (Exception _e)
            {
                conexionExceptionManager.throwExceptionInCatch(_e, this);
                MessageBox.Show(_e.Message);
            }

        }

        #endregion

        #region Metodos get 

        private void getTextField()
        {
            try
            {
                this.BaseEntity.strNombre = this.txtNombre.Text.Trim();
                this.BaseEntity.strAPaterno = this.txtApellidoPaterno.Text.Trim();
                this.BaseEntity.strAMaterno = this.txtApellidoMaterno.Text.Trim();
                this.BaseEntity.strCURP = this.txtCurp.Text.Trim();
                this.BaseEntity.strFolioIFE = this.txtFolioIfe.Text.Trim();
                this.BaseEntity.strOriginario = this.txtOriginario.Text.Trim();
                this.BaseEntity.strNombreCompleto = this.txtApellidoPaterno.Text.Trim() + " " + this.txtApellidoMaterno.Text.Trim() + " " + this.txtNombre.Text.Trim();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void getComboBox()
        {
            try
            {
                if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Agregar)
                {
                    this.BaseEntity.ComCatSexo = (ComCatSexo)this.cmbComCatSexo.SelectedItem;
                    this.BaseEntity.ComCatEstadoCivil = (ComCatEstadoCivil)this.cmbComCatEstadoCivil.SelectedItem;
                    this.BaseEntity.ComCatNacionalidad = (ComCatNacionalidad)this.cmbComCatNacionalidad.SelectedItem;
                    this.BaseEntity.ComCatOcupacion = (ComCatOcupacion)this.cmbComCatOcupacion.SelectedItem;
                    this.BaseEntity.ComCatRegimenMatrimonial = (ComCatRegimenMatrimonial)this.cmbComCatRegimenMatrimonial.SelectedItem;

                }
                if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Editar)
                {
                    this.BaseEntity.ComCatEstadoCivil = (ComCatEstadoCivil)this.cmbComCatEstadoCivil.SelectedItem;
                    this.BaseEntity.ComCatNacionalidad = (ComCatNacionalidad)this.cmbComCatNacionalidad.SelectedItem;
                    this.BaseEntity.ComCatOcupacion = (ComCatOcupacion)this.cmbComCatOcupacion.SelectedItem;
                    this.BaseEntity.ComCatRegimenMatrimonial = (ComCatRegimenMatrimonial)this.cmbComCatRegimenMatrimonial.SelectedItem;
                    this.BaseEntity.ComCatSexo = (ComCatSexo)this.cmbComCatSexo.SelectedItem;


                }



            }
            catch (Exception _e)
            {

                MessageBox.Show(_e.Message);
            }


        }

        private void getDatetime()
        {
            try
            {
                //String  date = ( dteFechaNacimiento.Value.Day +" /" + dteFechaNacimiento.Value.Month + " /" +
                //  dteFechaNacimiento.Value.Year).ToString();         
                this.BaseEntity.dteFechaNacimiento = this.dteFechaNacimiento.Value.Date;//Convert.ToDateTime(date);

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void getCheckbox()
        {
            try
            {
                this.BaseEntity.bitDocEstancia = this.chkDocumentoLegalEstancia.Checked;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }


        }

        #endregion

        #region Eventos Genericos

        private void btnAceptarClick(object sender, EventArgs e)
        {
            try
            {
                this.accionButton = EnumAccionButton.AceptarClick;
                if (this.tipoOperacion == EnumOperationType.Agregar)
                {
                    ///Unicamente cuando se agrega lleva el getEntityForm
                    this.getEntityForm();
                    if (this.validaEntidad())
                    {
                        if (this.saveEntity())
                        {
                            if (this.StateForm == EnumStateForm.WithChanges)
                            {
                                this.IgnoreClosing = true;
                            }
                            else
                            {
                                this.IgnoreClosing = false;
                            }
                            this.Close();
                            this.accionButton = EnumAccionButton.XClick;
                            return;
                        }
                    }
                    else
                    {
                        if (this.StateForm == EnumStateForm.WithChanges)
                        {
                            this.AccionDialog = EnumAccionDialog.Null;
                            this.Close();
                            this.accionButton = EnumAccionButton.XClick;
                        }
                        if (this.StateForm == EnumStateForm.WithOutChanges)
                        {

                            this.AccionDialog = EnumAccionDialog.Null;
                            this.Close();
                            this.accionButton = EnumAccionButton.XClick;
                            this.AccionDialog = EnumAccionDialog.Vacio;
                        }


                    }

                }

                if (this.tipoOperacion == EnumOperationType.Editar)
                {
                    if (this.stateForm == EnumStateForm.WithChanges)
                    {
                        if (this.validaEntidad())
                        {
                            if (this.saveEntity())
                            {
                                if (this.StateForm == EnumStateForm.WithChanges)
                                {
                                    this.IgnoreClosing = true;
                                }
                                else
                                {
                                    this.IgnoreClosing = false;
                                }

                                this.Close();
                                this.accionButton = EnumAccionButton.XClick;
                                return;
                            }
                        }
                        else
                        {
                            if (this.StateForm == EnumStateForm.WithChanges)
                            {
                                this.AccionDialog = EnumAccionDialog.Null;
                                this.Close();
                                this.accionButton = EnumAccionButton.XClick;
                            }

                        }
                    }
                    if (this.stateForm == EnumStateForm.WithOutChanges)
                    {
                        this.Close();
                        this.accionButton = EnumAccionButton.XClick;
                        this.AccionDialog = EnumAccionDialog.Vacio;
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void btnCancelarClick(object sender, EventArgs e)
        {
            try
            {

                this.accionButton = EnumAccionButton.CancelarClick;
                /// entra  sin cambios
                if (this.StateForm == EnumStateForm.WithOutChanges)
                {
                    this.StateForm = EnumStateForm.WithOutChanges;
                }

                ///entra con cambios
                if (this.StateForm == EnumStateForm.WithChanges)
                {

                    DialogResult result;
                    if (this.tipoOperacion == EnumOperationType.Agregar)
                    {
                        result = MessageBox.Show(Not.Control.Comun.Properties.Resources.PRE_CIERRE_NUEVO,
                            Not.Control.Comun.Properties.Resources.MES_TITULO, MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        result = MessageBox.Show(Not.Control.Comun.Properties.Resources.PRE_CIERRE_EDITAR,
                            Not.Control.Comun.Properties.Resources.MES_TITULO, MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Information);
                    }

                    if (result == DialogResult.Yes)
                    {
                        if (this.tipoOperacion == EnumOperationType.Agregar)
                        {
                            this.getEntityForm();
                            if (this.validaEntidad())
                            {
                                this.StateForm = EnumStateForm.WithChanges;
                                this.AccionDialog = EnumAccionDialog.Si;
                                this.Close();
                                this.accionButton = EnumAccionButton.XClick;
                            }
                            else
                            {
                                this.StateForm = EnumStateForm.WithChanges;
                                this.AccionDialog = EnumAccionDialog.Null;
                                this.Close();
                                this.accionButton = EnumAccionButton.XClick;
                            }
                            return;

                        }
                        if (this.tipoOperacion == EnumOperationType.Editar)
                        {

                            if (this.validaEntidad())
                            {
                                this.StateForm = EnumStateForm.WithChanges;
                                this.AccionDialog = EnumAccionDialog.Si;
                                this.Close();
                                this.accionButton = EnumAccionButton.XClick;
                            }
                            else
                            {
                                this.StateForm = EnumStateForm.WithChanges;
                                this.AccionDialog = EnumAccionDialog.Null;
                                this.Close();
                                this.accionButton = EnumAccionButton.XClick;
                            }


                            return;
                        }
                    }

                    if (result == DialogResult.No)
                    {
                        this.StateForm = EnumStateForm.WithChanges;
                        this.AccionDialog = EnumAccionDialog.No;
                        this.BaseEntity = null;

                    }

                    if (result == DialogResult.Cancel)
                    {
                        this.StateForm = EnumStateForm.WithChanges;
                        this.AccionDialog = EnumAccionDialog.Cancel;
                        this.Close();
                        this.accionButton = EnumAccionButton.XClick;
                        return;
                    }
                }
                if (this.stateForm == EnumStateForm.Null)
                {

                    this.Close();
                    this.accionButton = EnumAccionButton.XClick;
                    return;

                }

                this.Close();
                this.accionButton = EnumAccionButton.XClick;

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void txtChanged(object sender, EventArgs e)Aqiiiiii!!!
        {
            if (this.validateStateFormControl == true)
            {
            this.StateForm = EnumStateForm.WithChanges;
            this.StateEdit = true;
        }

        }

        private void cmbChanged(object sender, EventArgs e)
        {
            if (this.validateStateFormControl == true)
            {
                this.StateForm = EnumStateForm.WithChanges;
                this.StateEdit = true;
            }

        }

        private void dteChanged(object sender, EventArgs e)
        {
            if (this.validateStateFormControl == true)
            {
                this.StateForm = EnumStateForm.WithChanges;
                this.StateEdit = true;
            }

        }

        private void chkChanged(object sender, EventArgs e)
        {
            if (this.validateStateFormControl == true)
            {
                this.StateForm = EnumStateForm.WithChanges;
                this.StateEdit = true;
            }

        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {

            if (this.accionButton == EnumAccionButton.AceptarClick)
            {
                this.formClosing(e);

            }
            if (this.accionButton == EnumAccionButton.CancelarClick)
            {
                this.formClosing(e);

            }
            if (this.accionButton == EnumAccionButton.XClick)
            {
                if (this.StateForm == EnumStateForm.WithChanges)
                {
                    this.btnCancelarClick(sender, e);
                    this.formClosing(e);
                    this.accionButton = EnumAccionButton.XClick;
                }
                if (this.StateForm == EnumStateForm.WithOutChanges)
                {
                    this.formClosing(e);
                }

            }

        }
        #endregion

    }
}
