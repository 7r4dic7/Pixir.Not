#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Linq;
using Pixir.Not.Control.Interface.Comun;
using Pixir.Not.Data.Entity;
using System.Linq.Expressions;
using Pixir.Not.View.Extended.View;
using Pixir.Not.View.Extended.Disconnect.Catalog;

#endregion
namespace Not.View.Views.Common.Persona
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmPersonaPrincipal : Form, IControlRegister<Pixir.Not.Data.Entity.ComPersona>
    {

        #region Variables

        /// <summary>
        /// Variable que representa el punto de entrada de linq.
        /// </summary>
        private DataContext dataContext;

        /// <summary>
        /// Variable de tipo CatalogExchange. 
        /// </summary>
        private CatalogExchange estadoCivilCriteria;

        /// <summary>
        /// Variable de tipo CatalogExchange. 
        /// </summary>
        private CatalogExchange sexoCriteria;

        /// <summary>
        /// Vriable de tipo String. almacena la cadena de busqueda.
        /// </summary>
        private String textoCriteria;

        /// <summary>
        /// Variable de tipo ComPersona.  Hace referencia a la entidad base o principal del modulo.
        /// </summary>
        private Pixir.Not.Data.Entity.ComPersona baseEntity;

        /// <summary>
        /// Variable de tipo System.Windows.Forms.Timer.
        /// </summary>
        System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer();


        /// <summary>
        /// Variable  de tipo PermisosPantalla. Contiene la logica de los permisos.
        /// </summary>
        //private PermisosPantalla permisosPantalla = null;

        /// <summary>
        /// variable que contiene todas las instancia de la interface que se implementan
        /// </summary>
        private List<IRegisterRules> reglasSeleccion = new List<IRegisterRules>();
        /// <summary>
        /// Variable que indica si ya fue asignados los datos a otro modulo
        /// </summary>
        private bool asignData = false;

        /// <summary>
        /// Variable que inicializa el estado registro, como activo
        /// </summary>
        private Pixir.Not.Data.Entity.ComCatEstadoRegistro estadoRegistroCriteria = new Pixir.Not.Data.Entity.ComCatEstadoRegistro();

        /// <summary>
        /// Variable que indica si ce cerro la ventana, al pulsal el boton X
        /// </summary>
        private bool closeWindow = false;

        /// <summary>
        /// Variabale que indica que se ha seleccionado el filtro sexo, la seleccion es diferente de todos
        /// </summary>
        private bool inSexo = false;

        /// <summary>
        /// Variable que indica que se ha seleccionado el  filtro estado civil, la seleccion es diferente de todos
        /// </summary>
        private bool inEstadoCivil = false;

        /// <summary>
        /// Variable que indica si la consulta en el setResultadoEmpleado
        /// se ejecutara en la forma abierta o en la cerrada, donde abierta es igual a entrar al databinding normal o 
        /// cerrada donde entra a datbinding source especifico, dependiendo de las combinaciones. cuando es true no entra 
        /// si trae datso tipo texto en la consulta
        /// </summary>
        private bool inSetResultado = false;

        /// <summary>
        /// Variable que lleva el conteo de cuantas ocaciones se accede
        /// a setResultadoPersona al cargar la pantalla
        /// </summary>
        private int countSetResultadoGlobal = (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero;

        /// <summary>
        /// variable que verifica si se ha dado clic al boton seleccionar
        /// </summary>
        private bool buttonSelect = false;

        /// <summary>
        /// variable que indica si se ha pilsado el boton salir
        /// </summary>
        private bool buttonExit = false;

        /// <summary>
        /// DataContext que permite realizar la eliminacion
        /// </summary>
        private DataContext dataContextDelete;

        /// <summary>
        /// variable que contiene el mensaje para la excepcion del filtro.
        /// </summary>
        private String messageFilterException = String.Empty;

        /// <summary>
        /// Variable de tipo filterExceptionPrincipal.
        /// </summary>
        //private FilterExceptionPrincipal filterExceptionPrincipal = new FilterExceptionPrincipal();

        /// <summary>
        /// Variable de tipo conexionExceptionPrincipal.
        /// </summary>
        //private ConexionExceptionPrincipal conexionExceptionPrincipal = new ConexionExceptionPrincipal();

        /// <summary>
        /// Variable que almacena el usuario 
        /// </summary>
        //private SegUsuario segUsuario;

        /// <summary>
        /// Objeto que establece la instancia de la clase que contiene la sentncia precompilda para principal.
        /// </summary>
        Pixir.Not.View.Common.Compiled<Pixir.Not.Data.Entity.ComPersona> queryCompiled = new Pixir.Not.View.Common.Compiled<ComPersona>();
        #endregion

        #region Propiedades


        public DataContext DataContext
        {
            get { return dataContext; }
            set { dataContext = value; }
        }

        /// <summary>
        /// Propiedad que obtiene la refencia de todas las implementaciones de esta interface
        /// </summary>     
        public List<IRegisterRules> ReglasSeleccion
        {
            get { return reglasSeleccion; }
            set { reglasSeleccion = value; }
        }


        #endregion

        #region Constructor


        public frmPersonaPrincipal()
        {
            InitializeComponent();
            this.loadInitialInformation();
        }
        #endregion

        #region Metodo loadInitialInformation

        private void loadInitialInformation()
        {
            try
            {
                this.DataContext = new DcGeneralDataContext();
                this.setFiltroComCatEstadoCivil();
                this.setFiltroComCatSexo();
                this.setResultadoComPersona();
                this.hidePanelMessage();
                //this.permisosPantalla = _permisosPantalla;
                this.setStatus();
                this.setTabIndex();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        #endregion

        #region Metodo setStatus

        private void setStatus()
        {
            this.lblNumeroFilas.Location = new Point((int)Pixir.Not.Data.Extended.Enum.EnumLocation.PositionNumberFile_X,
                (int)Pixir.Not.Data.Extended.Enum.EnumLocation.PositionNumberFile_Y);
            this.btnSeleccionar.Visible = false;
            this.btnSalir.Visible = false;
            //this.setPermisosPantalla();
        }

        #endregion

        #region  Metodo setTabIndex

        /// <summary>
        /// metodo que establece el Tabindex de los controles, en forma manual.
        /// </summary>
        private void setTabIndex()
        {
            //Controles superiores
            this.btnSalir.TabIndex = 1;
            this.btnAgregar.TabIndex = 2;
            this.pnlBusqueda.TabIndex = 3;
            this.txtCriteria.TabIndex = 4;
            this.btnActualizar.TabIndex = 5;
            this.btnSeleccionar.TabIndex = 6;
            this.btnEditar.TabIndex = 7;
            this.btnEliminar.TabIndex = 8;

            //Filtros
            this.dgvFiltroComCatSexo.TabIndex = 9;
            this.dgvFiltroComCatEstadoCivil.TabIndex = 10;
            //resultado
            this.dgvResultadoPersona.TabIndex = 11;

            //Detalle
            this.pnlPrincipalDetalle.TabIndex = 12;
            this.btnImprimir.TabIndex = 13;
            //Detalle datagridview
            this.dgvDetalleComDatoContacto.TabIndex = 14;

            //Botones inferiores
            this.btnSms.TabIndex = 15;
            this.btnCorreoElectronico.TabIndex = 16;



        }


        #endregion

        #region Metodo setDataContextRefresh

        /// <summary>
        /// Metodo que refresca el data context. la baseEntity y los datos que se muestran el data grids
        /// </summary>
        private void setDataContextRefresh()
        {
            try
            {
                if (this.dgvResultadoPersona.RowCount > (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    this.dataContext.Refresh(RefreshMode.OverwriteCurrentValues, this.baseEntity);
                    for (int i = 0; i < this.baseEntity.ComDatoContacto.Count; i++)
                    {
                        ComDatoContacto comDatoContacto = new ComDatoContacto();
                        comDatoContacto = this.baseEntity.ComDatoContacto[i];
                        this.dataContext.Refresh(RefreshMode.OverwriteCurrentValues, comDatoContacto);
                    }
                    //for (int i = 0; i < this.baseEntity.CliPersonaCliente.Count; i++)
                    //{
                    //    for (int j = 0; j < this.baseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto.Count; j++)
                    //    {
                    //        ComDatoContacto comDatoContacto = new ComDatoContacto();
                    //        comDatoContacto = this.baseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto[j];
                    //        this.dataContext.Refresh(RefreshMode.OverwriteCurrentValues, this.baseEntity.CliPersonaCliente[i].CliCliente.ComDatoContacto[j]);
                    //    }
                    //}
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }
        #endregion

        #region Metodos setFiltro


        private void setFiltroComCatEstadoCivil()
        {
            try
            {
                List<CatalogExchange> list = CtrlCatalogFilter.getCatalog<ComCatEstadoCivil>(this.DataContext).ToList();
                if (list.Count == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    //this.messageFilterException = Resources.MES_COM_CAT_ESTADO_CIVIL_VACIO;
                    //filterExceptionPrincipal.throwExceptionInFilter();
                }

                list.Insert(0, new CatalogExchange()
                {
                    id = (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.MenosUno,
                    strValor = Pixir.Not.Data.Extended.Enum.EnumFilterHead.TODOS.ToString()
                });
                dgvFiltroComCatEstadoCivil.DataSource = list;
                dgvFiltroComCatEstadoCivil.Rows[0].Selected = true;

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setFiltroComCatSexo()
        {
            try
            {
                List<CatalogExchange> list = CtrlCatalogFilter.getCatalog<ComCatSexo>(this.DataContext).ToList();
                if (list.Count == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    //this.messageFilterException = Resources.MES_COM_CAT_SEXO_VACIO;
                    //filterExceptionPrincipal.throwExceptionInFilter();
                }
                list.Insert(0, new CatalogExchange()
                {
                    id = (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.MenosUno,
                    strValor = Pixir.Not.Data.Extended.Enum.EnumFilterHead.TODOS.ToString()
                });
                dgvFiltroComCatSexo.DataSource = list;
                dgvFiltroComCatSexo.Rows[0].Selected = true;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setFiltroComCatEstadoRegistro()
        {
            try
            {
                this.estadoRegistroCriteria.id = (int)Pixir.Not.Data.Generic.EnumRegisterState.Activo;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }


        #endregion

        #region Metodo setResultado


        private void setResultadoComPersona()
        {

            bool estadoEstadoCivilCriteria = false;
            bool estadoSexoCriteria = false;
            bool estadoTextoCriteria = false;

            int valueEstadoCivilCriteria = (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero;
            string valueTextoCriteria = String.Empty;
            int valueSexoCriteria = (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero;

            if (this.estadoCivilCriteria != null)
            {
                estadoEstadoCivilCriteria = true;
                valueEstadoCivilCriteria = this.estadoCivilCriteria.id;
            }
            if (this.sexoCriteria != null)
            {
                estadoSexoCriteria = true;
                valueSexoCriteria = this.sexoCriteria.id;
            }
            if (!this.txtCriteria.Text.Trim().Equals(""))
            {
                estadoTextoCriteria = true;
            }
            try
            {
                Expression<Func<ComPersona, bool>> predicate = c => (
                ((estadoEstadoCivilCriteria) ? c.ComCatEstadoCivil.id == valueEstadoCivilCriteria : true) &&
                ((estadoSexoCriteria) ? c.ComCatSexo.id == valueSexoCriteria : true) &&
                ((estadoTextoCriteria) ? (((estadoTextoCriteria) ? c.strNombreCompleto.Contains(txtCriteria.Text.Trim()) : false))
                : true)
                );
                this.countSetResultadoGlobal++;
                List<ComPersona> lista = this.queryCompiled.getListEntity(this.DataContext).Where(predicate).OrderBy(p => p.strAPaterno).ToList();
                //List<ComPersona> lista = Not.View.Common.Compiled<ComPersona>.generateQueryCompile(this.dataContext, predicate);
                //    //.getListEntity(this.DataContext).Where(predicate).OrderBy(p => p.strAPaterno).ToList();
                this.dgvResultadoPersona.DataSource = lista;
                this.lblNumeroFilas.Text = "{" + this.dgvResultadoPersona.RowCount.ToString() + "}";
                this.busquedaSinDatos();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        /// <summary>
        /// Limpia las etiquetas y los data gridviews de el area de detalle, si no existe coincidencia en los
        /// datos de busqueda
        /// </summary>
        private void limpiarSetDetalle()
        {
            try
            {
                this.lblNombrePrincipal.Text = String.Empty;
                this.lblNombreMuestra.Text = String.Empty;
                this.lblCurpMuestra.Text = String.Empty;
                this.lblFolioIfeMuestra.Text = String.Empty;
                this.lblOriginarioMuestra.Text = String.Empty;
                this.lblFechaNacimientoMuestra.Text = String.Empty;
                this.lblDocLegEstMuestra.Text = String.Empty;
                this.lblNacionalidadMuestra.Text = String.Empty;
                this.lblOcupacionMuestra.Text = String.Empty;
                this.lblSexoMuestra.Text = String.Empty;
                this.lblRegimenMatrimonialMuestra.Text = String.Empty;
                this.lblEstadoCivilMuestra.Text = String.Empty;
                this.dgvDetalleComDatoContacto.DataSource = null;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        /// <summary>
        /// Metodo que limpia el area de detalle y muesta la el error provider que indica que no existe
        /// datos para la consulta.
        /// </summary>
        private void busquedaSinDatos()
        {
            try
            {
                if (countSetResultadoGlobal >= (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Uno)
                {
                    if (this.dgvResultadoPersona.Container == null &&
                        this.dgvResultadoPersona.Rows.Count == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                    {
                        this.limpiarSetDetalle();
                        //this.eprAvisoBusqueda.SetError(this.pnlBusqueda, Resources.MES_BUSQUEDA_VACIA);
                    }
                    else
                    {
                       // this.eprAvisoBusqueda.SetError(this.pnlBusqueda, String.Empty);
                    }
                }
                else
                {
                    //this.eprAvisoBusqueda.SetError(this.pnlBusqueda, String.Empty);
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }
        #endregion

        #region Metodos setDetalle

        private void loadEntity(Pixir.Not.Data.Entity.ComPersona _entity)
        {
            this.baseEntity = _entity;

            this.setDetalleTextBox(_entity);
            this.setDetalleComboBox(_entity);
            this.setDetalleDateTime(_entity);
            this.setDetalleCheckBox(_entity);
            this.setDetalleDataGridView(_entity);

        }

        private void setDetalleTextBox(Pixir.Not.Data.Entity.ComPersona _entity)
        {
            try
            {

                this.lblNombrePrincipal.Text = _entity.strAPaterno + " " + _entity.strAMaterno + " " +
                     _entity.strNombre ?? String.Empty;
                this.lblNombreMuestra.Text = _entity.strAPaterno + " " + _entity.strAMaterno + " " +
                     _entity.strNombre ?? String.Empty;
                this.lblCurpMuestra.Text = _entity.strCURP ?? String.Empty;
                this.lblFolioIfeMuestra.Text = _entity.strFolioIFE ?? String.Empty;
                this.lblOriginarioMuestra.Text = _entity.strOriginario ?? String.Empty;


            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setDetalleDateTime(Pixir.Not.Data.Entity.ComPersona _entity)
        {
            try
            {
                this.lblFechaNacimientoMuestra.Text = (_entity.dteFechaNacimiento == null) ? " " :
                    _entity.dteFechaNacimiento.Value.ToShortDateString();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setDetalleCheckBox(Pixir.Not.Data.Entity.ComPersona _entity)
        {
            try
            {
                if (_entity.ComCatNacionalidad != null)
                {

                    //muestra los datos si la nacionalidad es diferente de mexicana
                    if (!_entity.ComCatNacionalidad.strValor.Equals(Pixir.Not.Data.Extended.Enum.EnumNationality.MEXICANA.ToString()))
                    {
                        this.lblDocLegEstMuestra.Text = ((_entity.bitDocEstancia == false) ? "No" : "Si");
                    }
                    else
                    {
                        this.lblDocLegEstMuestra.Text = String.Empty;
                    }
                }
                else
                {
                    this.lblDocLegEstMuestra.Text = String.Empty;
                }


            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setDetalleComboBox(Pixir.Not.Data.Entity.ComPersona _entity)
        {
            try
            {

                if (_entity.ComCatNacionalidad != null)
                {
                    this.lblNacionalidadMuestra.Text = _entity.ComCatNacionalidad.strValor ?? String.Empty;
                }
                else
                {
                    this.lblNacionalidadMuestra.Text = String.Empty;
                }
                if (_entity.ComCatOcupacion != null)
                {
                    this.lblOcupacionMuestra.Text = _entity.ComCatOcupacion.strValor ?? String.Empty;
                }
                else
                {
                    this.lblOcupacionMuestra.Text = String.Empty;

                }

                if (_entity.ComCatSexo != null)
                {
                    this.lblSexoMuestra.Text = _entity.ComCatSexo.strValor ?? String.Empty;
                }
                else
                {
                    this.lblSexoMuestra.Text = String.Empty;
                }

                if (_entity.ComCatRegimenMatrimonial != null)
                {
                    this.lblRegimenMatrimonialMuestra.Text = _entity.ComCatRegimenMatrimonial.strValor ?? String.Empty;

                }
                else
                {
                    this.lblRegimenMatrimonialMuestra.Text = String.Empty;
                }
                if (_entity.ComCatEstadoCivil != null)
                {
                    this.lblEstadoCivilMuestra.Text = _entity.ComCatEstadoCivil.strValor ?? String.Empty;

                }
                else
                {
                    this.lblEstadoCivilMuestra.Text = String.Empty;
                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void setDetalleDataGridView(Pixir.Not.Data.Entity.ComPersona _entity)
        {
            try
            {
                //Contacto
                //Expression<Func<CliPersonaCliente, bool>> predicateCliente = c => c.ComPersona.id == _entity.id;
                //CliPersonaCliente personaCliente = Pixir.Not.Control.CtrlGeneric.getItemByExpresssion<CliPersonaCliente>(this.DataContext, predicateCliente);
                //bool isCliente = (personaCliente != null) ? true : false;
                //List<ComDatoContacto> comDatoContacto = new List<ComDatoContacto>();
                //Expression<Func<ComDatoContacto, bool>> predicateDatoContactoPersona = c => c.ComPersona.id == _entity.id;
                //Expression<Func<ComDatoContacto, bool>> predicateDatoContactoCliente = c => c.CliCliente.id == personaCliente.CliCliente.id && c.ComPersona == null;

                //comDatoContacto = Not.Control.CtrlGeneric.getListByExpression<ComDatoContacto>(this.DataContext, predicateDatoContactoPersona).ToList();
                //if (isCliente)
                //{
                //    comDatoContacto.AddRange(Not.Control.CtrlGeneric.getListByExpression<ComDatoContacto>(this.DataContext, predicateDatoContactoCliente).ToList());
                //}
                //this.dgvDetalleComDatoContacto.DataSource = comDatoContacto;

            }
            catch (Exception _e)
            {
                //if (this.conexionExceptionPrincipal.messajeConexionException(_e, this))
                //{
                //    this.limpiarSetDetalle();
                //    return;
                //}
                MessageBox.Show(_e.Message);
            }

        }

        #endregion

        #region Eventos frmClientePersonaPrincipal 

        private void txtCriteria_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.textoCriteria = txtCriteria.Text.Trim();
                if (txtCriteria.Text.Trim().Equals(String.Empty))
                {
                    this.textoCriteria = null;
                }

                this.setResultadoComPersona();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

      //  private void btnAgregar_Click(object sender, EventArgs e)
        //{
          //  try
           // {
            //    frmPersonaManager view = new frmPersonaManager();
            //    DataContext dataContextTemp = new DCGeneralDataContext();

            //    Not.Data.Entity.ComPersona entity = view.show(this, dataContextTemp, Not.Data.Extended.Enum.EnumOperationType.Agregar, this.segUsuario, null);

            //    if (entity != null)
            //    {
            //        dataContextTemp.SubmitChanges();
            //        this.getPanel(Not.Control.Comun.Properties.Resources.MES_OPERACION_EXITO_GUARDAR);
            //        this.setResultadoComPersona();
            //        dataContextTemp = null;
            //    }
            //    else
            //    {
            //        this.setDataContextRefresh();
            //        this.setResultadoComPersona();
            //    }
            //    this.btnAgregar.Focus();

            //}
            //catch (Exception _e)
            //{
            //    if (this.conexionExceptionPrincipal.messajeConexionException(_e, this)) { return; }
            //    MessageBox.Show(_e.Message);
            //}

       // }

        //private void btnEditar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.dgvResultadoPersona.SelectedRows.Count == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
        //        {
        //            //MessageBox.Show(this, Resources.MES_NO_EXISTE_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
        //            //    MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            if (this.baseEntity != null)
        //            {
        //                frmPersonaManager view = new frmPersonaManager();
        //                DataContext dataContextTemp = new DCGeneralDataContext();
        //                ComPersona persona = (ComPersona)this.dgvResultadoPersona.SelectedRows[0].DataBoundItem;
        //                Expression<Func<ComPersona, bool>> predicate = c => c.id == persona.id;
        //                ComPersona person = CtrlGeneric.getItemByExpresssion<ComPersona>(dataContextTemp, predicate);

        //                if (view.show(this, person, dataContextTemp, Not.Data.Extended.Enum.EnumOperationType.Editar, this.segUsuario, null) != null)
        //                {
        //                    dataContextTemp.SubmitChanges();
        //                    this.getPanel(Not.Control.Comun.Properties.Resources.MES_OPERACION_EXITO_EDITAR);
        //                    this.setDataContextRefresh();
        //                    this.setResultadoComPersona();
        //                    dataContextTemp = null;
        //                }
        //                else
        //                {
        //                    this.setDataContextRefresh();
        //                    this.setResultadoComPersona();
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_ENTIDAD_VACIA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
        //                 MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception _e)
        //    {

        //        if (this.conexionExceptionPrincipal.messajeConexionException(_e, this)) { return; }
        //        MessageBox.Show(_e.Message);
        //    }



        //}

        //private void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (this.dgvResultadoPersona.SelectedRows.Count == (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
        //        {
        //            MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_SELECCIONAR_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR,
        //                MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        else
        //        {
        //            DialogResult result = MessageBox.Show(Not.Control.Comun.Properties.Resources.PRE_ELIMINAR_REGISTRO, Resources.TIT_PERSONA_SINGULAR, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //            if (result == DialogResult.Yes)
        //            {

        //                this.dataContextDelete = new DCGeneralDataContext();
        //                ComPersona persona = (ComPersona)this.dgvResultadoPersona.SelectedRows[0].DataBoundItem;
        //                ComPersona person = this.dataContextDelete.GetTable<ComPersona>().Single(c => c.id == persona.id);
        //                this.dataContextDelete.GetTable<Not.Data.Entity.ComPersona>().DeleteOnSubmit(person);
        //                this.dataContextDelete.SubmitChanges();
        //                this.setResultadoComPersona();
        //                this.getPanel(Not.Control.Comun.Properties.Resources.MES_OPERACION_EXITO_ELIMINAR);
        //                this.dataContextDelete = null;


        //            }

        //        }

        //    }
        //    catch (Exception _e)
        //    {

        //        this.dataContextDelete = null;
        //        if (this.conexionExceptionPrincipal.throwExceptionInCatchDelete(_e, this)) { return; };
        //        if (_e.sqlExceptionDelete(this)) { return; }
        //        MessageBox.Show(_e.Message);
        //    }
        //}

        //private void btnActualizar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.dgvResultadoPersona.SelectedRows.Count == (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
        //        {
        //            MessageBox.Show(this, Resources.MES_NO_EXISTE_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
        //                MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            this.setDataContextRefresh();
        //            this.setResultadoComPersona();
        //            this.getPanel(Not.Control.Comun.Properties.Resources.MES_OPERACION_EXITO_ACTUALIZAR);
        //        }
        //    }
        //    catch (Exception _e)
        //    {

        //        if (this.conexionExceptionPrincipal.messajeConexionException(_e, this)) { return; }
        //        MessageBox.Show(_e.Message);
        //    }
        //}

        //private void btnImprimir_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.dgvResultadoPersona.SelectedRows.Count == (int)Not.Data.Extended.Enum.EnumNumericValue.Cero)
        //        {
        //            MessageBox.Show(this, Resources.MES_NO_EXISTE_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
        //                MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            if (this.baseEntity != null)
        //            {
        //                frmPersonaReport view = new frmPersonaReport();
        //                view.Show(this, this.baseEntity, this.dataContext);
        //            }
        //            else
        //            {
        //                MessageBox.Show(this, Not.Control.Comun.Properties.Resources.MES_ENTIDAD_VACIA, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
        //                 MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception _e)
        //    {
        //        MessageBox.Show(_e.Message);
        //    }
        //}

        /// <summary>
        /// Metodo que verifica que la empresa no se haya asignado anterior mente como cobDeposito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvResultadoPersona.SelectedRows.Count > (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    if (this.baseEntity == null)
                    {
                        //MessageBox.Show(this, Pixir.Not.Control.Comun.Properties.Resources.MES_SELECCIONAR_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        //    MessageBoxIcon.Error);
                        return;
                    }
                    this.buttonSelect = true;
                    if (this.reglasSeleccion != null)
                    {
                        foreach (IRegisterRules regla in this.reglasSeleccion)
                        {
                            if (!regla.validaRegistro(this.baseEntity, dataContext))
                            {
                                //MessageBox.Show(this, regla.Mensaje, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.asignData = true;
                                this.buttonSelect = false;
                                break;

                            }
                            else
                            {
                                this.asignData = false;
                                this.Close();
                                break;
                            }
                        }
                    }

                }
                else
                {
                   // MessageBox.Show(this, Resources.MES_NO_EXISTE_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void btnSms_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvResultadoPersona.SelectedRows.Count == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    //MessageBox.Show(this, Resources.MES_NO_EXISTE_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information);
                }
                else
                {
                    if (this.baseEntity != null)
                    {
                        //if (this.segUsuario != null)
                        //{
                        //    CatalogoEnvio catalogoEnvio = new CatalogoEnvio();
                        //    catalogoEnvio.IdSegModulo = (int)Not.Data.Extended.Enum.EnumSegModulo.PERSONA;
                        //    catalogoEnvio.IdConCatTabla = (int)Not.Data.Extended.Enum.EnumConCatTabla.ComPersona;
                        //    catalogoEnvio.IntId = this.baseEntity.id;
                        //    frmSmsEnvioManualManager view = new frmSmsEnvioManualManager();
                        //    view.show(this, this.baseEntity, this.segUsuario, catalogoEnvio, this.DataContext, Not.Data.Extended.Enum.EnumOperationType.Editar);
                        //}
                        //if (this.segUsuario == null)
                        //{
                        //    MessageBox.Show(this, Resources.MES_USUARIO_INVALIDO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                        //        MessageBoxIcon.Information);
                        //}
                    }
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void btnCorreoElectronico_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvResultadoPersona.SelectedRows.Count == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.Cero)
                {
                    //MessageBox.Show(this, Resources.MES_NO_EXISTE_REGISTRO, Not.Control.Comun.Properties.Resources.TIT_VERIFICAR, MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information);
                }
                else
                {
                    //if (this.baseEntity != null)
                    //{

                    //    CatalogoEnvio catalogoEnvio = new CatalogoEnvio();
                    //    catalogoEnvio.IdSegModulo = (int)Not.Data.Extended.Enum.EnumSegModulo.PERSONA;
                    //    catalogoEnvio.IdConCatTabla = (int)Not.Data.Extended.Enum.EnumConCatTabla.ComPersona;
                    //    catalogoEnvio.IntId = this.baseEntity.id;
                    //    Not.View.Views.Common.EMail.frmEMailEnvioManualManager view = new Not.View.Views.Common.EMail.frmEMailEnvioManualManager();
                    //    view.show(this, this.baseEntity, this.segUsuario, catalogoEnvio, this.DataContext, Not.Data.Extended.Enum.EnumOperationType.Editar);
                    //}
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }



        }

        /// <summary>
        /// Evento que cierra la pantalla, cuando se asignan datos a otro Proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonExit = true;
                this.Close();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        #endregion

        #region Eventos DatagridView


        private void dgvFiltroComCatEstadoCivil_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.estadoCivilCriteria = (CatalogExchange)this.dgvFiltroComCatEstadoCivil.Rows[e.RowIndex].DataBoundItem;
                if (this.estadoCivilCriteria.id == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.MenosUno)
                {
                    this.estadoCivilCriteria = null;
                }
                this.setResultadoComPersona();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void dgvFiltroComCatSexo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                this.sexoCriteria = (CatalogExchange)this.dgvFiltroComCatSexo.Rows[e.RowIndex].DataBoundItem;
                if (this.sexoCriteria.id == (int)Pixir.Not.Data.Extended.Enum.EnumNumericValue.MenosUno)
                {
                    this.sexoCriteria = null;
                }
                this.setResultadoComPersona();

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }

        private void dgvResultadoPersona_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.loadEntity((ComPersona)this.dgvResultadoPersona.Rows[e.RowIndex].DataBoundItem);
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }


        }

        private void cellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ((DataGridView)sender).Rows[e.RowIndex].Selected = true;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }
        }
        #endregion

        #region Metodos setMensaje

        private void hidePanelMessage()
        {
            try
            {
                this.pnlMesOperacion.setLocation();
                this.pnlMesOperacion.setSize();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void setTimer()
        {
            try
            {
                clock.Tick += new EventHandler(timMenOperacion_Tick);
                clock.Enabled = true;
                clock.Interval = (int)Pixir.Not.Data.Extended.Enum.EnumTime.IntervaloMensajePrincipal;
                clock.Start();


            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        private void getPanel(String _message)
        {
            try
            {
                PanelMessagePrincipal.setVisiblePanelAndLabel(this.pnlMesOperacion, this.lblMensajeOperacion, _message);
                this.setTimer();
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }


        }

        private void timMenOperacion_Tick(object sender, EventArgs e)
        {
            this.hidePanelMessage();
        }

        #endregion

        #region  Pantalla Emergente Persona

        public Pixir.Not.Data.Entity.ComPersona Show(Form _padre)
        {
            try
            {
                this.cargaControlesBase();
                if (_padre != null)
                {

                    this.AutoSize = true;
                    this.StartPosition = FormStartPosition.CenterParent;
                    this.WindowState = FormWindowState.Normal;
                    this.Size = new System.Drawing.Size((int)Pixir.Not.Data.Extended.Enum.EnumScreenSize.SizeXVentanaAsignar,
                        (int)Pixir.Not.Data.Extended.Enum.EnumScreenSize.SizeYVentanaAsignar);
                    if (this.Size.Height < 1600)
                    {
                        this.Size = new System.Drawing.Size((int)Pixir.Not.Data.Extended.Enum.EnumScreenSize.SizeXVentanaAsignar,
                        (int)Pixir.Not.Data.Extended.Enum.EnumScreenSize.SizeYVentanaAsignar);
                    }
                    this.ShowDialog();
                }
                else
                {
                    this.Show();
                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

            if (this.asignData || this.closeWindow || this.buttonExit)
            {
                return null;
            }
            else
            {
                return this.baseEntity;
            }

        }



        /// <summary>
        /// Evento que nos indica si cerro la ventana, con el boton X
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">FormClosedEventArgs</param>
        private void frmPersonaPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.buttonSelect == true)
                {
                    this.closeWindow = false;
                }
                else
                {
                    this.closeWindow = true;
                }

            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
            }

        }

        /// <summary>
        /// Metodo que carga controles necesarios para el funcionamiento de la pantalla emeregente de
        /// asignacion de nuevos datos a otros modulos
        /// </summary>
        private void cargaControlesBase()
        {

            this.btnSeleccionar.Visible = true;
            this.btnSalir.Visible = true;
        }

        #endregion 

        #region SetPermiso

        private void setPermisosPantalla()
        {
            //this.btnAgregar.Visible = this.permisosPantalla.Agregar;
            //this.btnEditar.Visible = this.permisosPantalla.Editar;
            //this.btnEliminar.Visible = this.permisosPantalla.Eliminar;
            //this.btnImprimir.Visible = this.permisosPantalla.Imprimir;
            //this.btnSms.Visible = this.permisosPantalla.Sms;
            //this.btnCorreoElectronico.Visible = this.permisosPantalla.Mail;

        }
        #endregion

    }
}
