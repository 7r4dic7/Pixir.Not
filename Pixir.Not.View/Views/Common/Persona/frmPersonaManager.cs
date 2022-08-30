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
using System.Linq.Expressions;
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
        //private CatalogExceptionManager catalogExceptionManager = new CatalogExceptionManager();

        /// <summary>
        /// Variable de tipo ConexionExceptionManager.
        /// </summary>
        //private ConexionExceptionManager conexionExceptionManager = new ConexionExceptionManager();

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

        private void txtChanged(object sender, EventArgs e)
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

        #region Eventos Dato Contacto

        private void btnAgregarDatoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                frmDatoContactoManager view = new frmDatoContactoManager();
                ComDatoContacto entity = view.show(this, this.DataContext, Not.Data.Extended.Enum.EnumOperationType.Agregar, this.User, null);
                if (entity != null)
                {
                    //verifica la conexion con la bd
                    if (conexionExceptionManager.stateConexion(this.dataContext, this))
                    {
                        this.validateStateFormDataGrid = true;
                        this.BaseEntity.ComDatoContacto.Add(entity);
                        ((BindingSource)this.dgvComDatoContacto.DataSource).Add(entity);
                        this.dgvComDatoContacto.Refresh();
                    }

                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void btnEditarDatoContacto_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.dgvComDatoContacto.SelectedRows.Count == (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_SELECCIONAR_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                }
                else
                {
                    //verifica la conexion con la bd
                    if (conexionExceptionManager.stateConexion(this.dataContext, this))
                    {
                        frmDatoContactoManager view = new frmDatoContactoManager();
                        ComDatoContacto entity = (ComDatoContacto)this.dgvComDatoContacto.SelectedRows[0].DataBoundItem;
                        view.show(this, entity, this.DataContext, Not.Data.Extended.Enum.EnumOperationType.Editar, this.User, null);
                        if (view.StateEdit) { this.StateForm = EnumStateForm.WithChanges; }
                        this.dgvComDatoContacto.Refresh();

                    }


                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void btnEliminarDatoContacto_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvComDatoContacto.SelectedRows.Count == (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_SELECCIONAR_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show(Not.Control.Comun.Properties.Resources.PRE_ELIMINAR_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        //verifica la conexion con la bd
                        if (!conexionExceptionManager.stateConexion(this.dataContext, this)) { return; }
                        BindingSource binding = (BindingSource)dgvComDatoContacto.DataSource;

                        ComDatoContacto entity = (ComDatoContacto)this.dgvComDatoContacto.SelectedRows[0].DataBoundItem;
                        if (entity.id > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                        {
                            this.DataContext.GetTable<ComDatoContacto>().DeleteOnSubmit(entity);
                            this.validateStateFormDataGrid = true;
                            binding.Remove(entity);
                        }
                        else
                        {
                            if (this.BaseEntity.ComDatoContacto.Remove(entity))
                            {
                                this.validateStateFormDataGrid = true;
                                ((BindingSource)this.dgvComDatoContacto.DataSource).Remove(entity);
                            }
                        }
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        #endregion

        #region Eventos DataGridView

        private void cellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dg = (DataGridView)sender;
                dg.Rows[e.RowIndex].Selected = true;
            }

            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void rowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            if (this.validateStateFormDataGrid == true)
            {
                this.StateForm = EnumStateForm.WithChanges;
                this.stateEdit = true;
            }

        }

        private void rowRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            if (this.validateStateFormDataGrid == true)
            {
                this.StateForm = EnumStateForm.WithChanges;
                this.stateEdit = true;
            }

        }
        #endregion

        #region Metodos Validacion


        /// <summary>
        /// Metodo que valida que los datos, y a permite o no la ejecucion de saveEntity,
        /// </summary>
        /// <returns></returns>
        private bool validaEntidad()
        {
            try
            {

                //Verifica si la entidad esta vacia
                if (this.BaseEntity == null)
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_ENTIDAD_VACIA, Not.Control.Comun.Properties.Resources.TIT_SISTEMA,
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //valida los datos requeridos
                if (!this.validateNull())
                {
                    return false;
                }


                //valida los datos  mediante expresion regular
                if (!this.validateRegularExpresison())
                {
                    return false;
                }
                //Valida si existe una empresa duplicada
                if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Agregar)
                {
                    if (!this.validaPersonaDuplicadaInsert())
                    {
                        return false;
                    }
                }

                //Valida si existe una empresa duplicada
                if (this.tipoOperacion == Not.Data.Extended.Enum.EnumOperationType.Editar)
                {
                    if (!this.validaPersonaDuplicadaEdit())
                    {
                        return false;
                    }
                }


            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Metodo que valida si la empresa ya fue creada, al agregar
        /// </summary>
        /// <returns>bool</returns>
        private bool validaPersonaDuplicadaInsert()
        {

            try
            {
                CtrlPersona ctrlPersona = new CtrlPersona();
                Expression<Func<ComPersona, bool>> predicateExisteCurp = c => c.strCURP.Equals(this.txtCurp.Text.Trim());

                Expression<Func<ComPersona, bool>> predicate = this.obtenerExpresionPersonaDuplicada(this.baseEntity);
                List<ComPersona> lista = ctrlPersona.getListItemByExpression(this.dataContext, predicate);
                DialogResult dialogResult = DialogResult.Yes;

                int existCURP = Not.Control.CtrlGeneric.getListByExpression<ComPersona>(this.DataContext, predicateExisteCurp).ToList().Count;
                int existPersona = lista.Count;

                if (existCURP > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero && !this.txtCurp.Text.Trim().Equals(String.Empty))
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_CURP_DUPLICADA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.txtCurp.Focus();
                    return false;
                }
                if (existPersona > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        //persona igual, y curp son vacia
                        if (lista[i].strCURP.Trim().Equals(String.Empty) && this.txtCurp.Text.Trim().Equals(String.Empty))
                        {

                            MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURPS_VACIOS, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            this.txtCurp.Focus();
                            return false;
                        }
                        //CUANDO AMBAS CURPS SON LLEnAS 
                        if (!lista[i].strCURP.Trim().Equals(String.Empty) && !this.txtCurp.Text.Trim().Equals(String.Empty) && lista[i].strCURP.Trim().Equals(this.txtCurp.Text.Trim()))
                        {

                            MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURPS_LLENOS, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            this.txtCurp.Focus();
                            return false;
                        }

                        //el nuevo registro cuenta con CURP el de la bd no
                        if (lista[i].strCURP.Trim().Equals(String.Empty) && !this.txtCurp.Text.Trim().Equals(String.Empty))
                        {
                            dialogResult = MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURP_NUEVA_LLENA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.Yes)
                            {
                                return true;
                            }

                            this.txtCurp.Focus();
                            return false;
                        }

                        // si la curp en bd esta llena y en el nuevo registro esta vacia 
                        if (!lista[i].strCURP.Trim().Equals(String.Empty) && this.txtCurp.Text.Trim().Equals(String.Empty))
                        {
                            dialogResult = MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURP_ENBD_LLENA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.Yes)
                            {
                                return true;
                            }

                            this.txtCurp.Focus();
                            return false;
                        }
                    }
                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Metodo que valida si la empresa ya fue creada, al editar
        /// </summary>
        /// <returns>bool</returns>
        private bool validaPersonaDuplicadaEdit()
        {
            try
            {
                if (this.StateForm == EnumStateForm.WithChanges)
                {
                    CtrlPersona ctrlPersona = new CtrlPersona();
                    Expression<Func<ComPersona, bool>> predicate = this.obtenerExpresionPersonaDuplicada(this.baseEntity);
                    Expression<Func<ComPersona, bool>> predicateExisteCurp = c => c.strCURP.Equals(this.txtCurp.Text.Trim());

                    List<ComPersona> lista = ctrlPersona.getListItemByExpression(this.dataContext, predicate);
                    DialogResult dialogResult = DialogResult.Yes;

                    if (this.baseEntity.strCURP.Equals(String.Empty) && !this.txtCurp.Text.Trim().Equals(String.Empty))
                    {
                        int exist = Not.Control.CtrlGeneric.getListByExpression<ComPersona>(this.DataContext, predicateExisteCurp).ToList().Count;
                        if (exist > (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
                        {
                            MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_CURP_DUPLICADA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            this.txtCurp.Focus();
                            return false;
                        }
                    }
                    if (lista.Count >= (int)Not.Data.Extended.Enum.EnumNumericValue.Uno)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].id != this.BaseEntity.id)
                            {
                                //persona igual, y curp son vacia 
                                if (lista[i].strCURP.Trim().Equals(String.Empty) && this.txtCurp.Text.Trim().Equals(String.Empty))
                                {

                                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURPS_VACIOS, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                    this.txtCurp.Focus();
                                    return false;
                                }

                                //CUANDO AMBAS CURPS SON LLEnAS 
                                if (!lista[i].strCURP.Trim().Equals(String.Empty) && !this.txtCurp.Text.Trim().Equals(String.Empty) && lista[i].strCURP.Trim().Equals(this.txtCurp.Text.Trim()))
                                {

                                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURPS_LLENOS, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                    this.txtCurp.Focus();
                                    return false;
                                }

                                //el nuevo registro cuenta con CURP el de la bd no
                                if (lista[i].strCURP.Trim().Equals(String.Empty) && !this.txtCurp.Text.Trim().Equals(String.Empty))
                                {
                                    dialogResult = MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURP_NUEVA_LLENA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        return true;
                                    }

                                    this.txtCurp.Focus();
                                    return false;
                                }

                                // si la curp en bd esta llena y en el nuevo registro esta vacia 
                                if (!lista[i].strCURP.Trim().Equals(String.Empty) && this.txtCurp.Text.Trim().Equals(String.Empty))
                                {
                                    dialogResult = MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_PERSONA_DUPLICADA_CURP_ENBD_LLENA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        return true;
                                    }

                                    this.txtCurp.Focus();
                                    return false;
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Expresion que valida que la empresa no este duplicada en la BD
        /// </summary>
        /// <param name="_comPersona">ComPersona</param>
        /// <returns>Expression</returns>
        private Expression<Func<ComPersona, bool>> obtenerExpresionPersonaDuplicada(ComPersona _comPersona)
        {
            try
            {
                string strNombre = _comPersona.strNombre.Trim();
                string strAPaterno = _comPersona.strAPaterno.Trim();
                string strAMaterno = _comPersona.strAMaterno.Trim();
                int idSexo = _comPersona.idComCatSexo;
                DateTime? fechaNacimiento = _comPersona.dteFechaNacimiento.Value;

                Expression<Func<ComPersona, bool>> predicate = persona => (
                    persona.strNombre.Trim().Equals(strNombre) &&
                    persona.strAPaterno.Trim().Equals(strAPaterno) &&
                    persona.strAMaterno.Trim().Equals(strAMaterno) &&
                    persona.dteFechaNacimiento.Equals(fechaNacimiento) &&
                    persona.idComCatSexo == idSexo);
                return predicate;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
            return null;
        }

        /// <summary>
        /// metodo que valida que los datos necesarios requeridos en pantalla hallan sido capturados
        /// </summary>
        /// <returns>bool</returns>
        private bool validateNull()
        {
            try
            {
                if (this.txtNombre.Text.getTextFieldNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_NOMBRE_VACIO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.txtNombre.Focus();
                    return false;
                }
                if (this.txtApellidoPaterno.Text.getTextFieldNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_APELLIDO_PATERNO_VACIO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.txtApellidoPaterno.Focus();
                    return false;
                }
                if (!this.dteFechaNacimiento.getBirthDateMinimun(this))
                {
                    this.dteFechaNacimiento.Focus();
                    return false;
                }
                if (!this.cmbComCatSexo.getComboBoxNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_COMBOBOX_SEXO_SIN_SELECCIONAR, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.cmbComCatSexo.Focus();
                    return false;
                }

                if (!this.cmbComCatEstadoCivil.getComboBoxNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_COMBOBOX_ESTADO_CIVIL_SIN_SELECCIONAR, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbComCatEstadoCivil.Focus();
                    return false;
                }
                if (!this.cmbComCatOcupacion.getComboBoxNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_COMBOBOX_OCUPACION_SIN_SELECCIONAR, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbComCatOcupacion.Focus();
                    return false;
                }
                if (this.txtOriginario.Text.getTextFieldNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_ORIGINARIO_VACIO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.txtOriginario.Focus();
                    return false;
                }

                if (!this.cmbComCatNacionalidad.getComboBoxNull())
                {
                    MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_COMBOBOX_NACIONALIDAD_SIN_SELECCIONAR, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbComCatEstadoCivil.Focus();
                    return false;
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que los datos de los tipo texto sean correctos, mediante expresion regular
        /// </summary>
        /// <returns></returns>
        private bool validateRegularExpresison()
        {
            try
            {
                // si la curpe es diferente de vacia valida
                if (!this.txtCurp.Text.getTextFieldNull())
                {
                    if (this.txtCurp.Text.getCurpRegularExpression())
                    {
                        //MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_CURP_INCORRECTA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        //    MessageBoxIcon.Error);
                        this.txtCurp.Focus();
                        return false;
                    }

                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
                return false;
            }
            return true;

        }


        #endregion

    }
}
